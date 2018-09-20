using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kostagram_1._1.Models;

namespace Kostagram_1._1.Controllers
{
    [Authorize]
    public class TagoviController : Controller
    {
        private AplikacijaDb db = new AplikacijaDb();

        // GET: Tagovi
        public ActionResult Index()
        {
            return View(db.Tagovi.ToList());
        }

        [AllowAnonymous]
        public ActionResult PretragaTagova(string rec)
        {
            if (rec == "")
            {
                IEnumerable<Slika> celaLista = db.Slike.ToList();

                return View(celaLista);
            }
            IQueryable<Tag> listaTagova = db.Tagovi.Where(t => t.SadrzajTaga.Contains(rec));

            
            List<Tag> Tagovi = new List<Tag>();
            foreach (Tag t in listaTagova)
            {
                Tagovi.Add(t);
            }            

            List<Slika> listaSlika = new List<Slika>();
            foreach (Tag t in Tagovi)
            {
                foreach (TagRelacija tr in t.TagRelacije)
                {
                    Slika sl = db.Slike.Find(tr.SlikaId);
                    if (!listaSlika.Contains(sl))
                    {
                        listaSlika.Add(sl);
                    }
                    
                }
            }            

            return View(listaSlika);
            
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult TagoviZaPretragu(string q)
        {
            IQueryable<string> listaTagova = db.Tagovi.Where(t=>t.SadrzajTaga.Contains(q))
                .Select(t => t.SadrzajTaga);

            return Json(listaTagova, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int UbaciTagove(int SlikaId, string tagovi)
        {
            string[] pojedninacniTagovi = tagovi.Split('#');

            List<Tag> tagovi1 = new List<Tag>();

            foreach (string t in pojedninacniTagovi)
            {
                t.Trim();
                t.ToLower();

                if (string.IsNullOrWhiteSpace(t))
                {
                    continue;
                }

                Tag t1 = db.Tagovi.FirstOrDefault(f => f.SadrzajTaga == t);
                if (t1 == null)

                {                   
                        Tag t2 = new Tag();
                        t2.SadrzajTaga = t;
                        db.Tagovi.Add(t2);
                        tagovi1.Add(t2);                                           
                }
                else
                {
                    //Tag t2 = new Tag();
                    //t2.SadrzajTaga = t;
                    tagovi1.Add(t1);                    
                }
              
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return 1;
            }

            foreach (var t in tagovi1)
            {
                TagRelacija tr = new TagRelacija();

                tr.SlikaId = SlikaId;
                tr.TagId = t.TagId;
                db.TagRelacije.Add(tr);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return 1;
            }


            return 1;
        }

        public int IzmeniTagove(int SlikaId, string tagovi)
        {
            List<Tag> listaTagova = TagoviZaSliku(SlikaId);
           
            string[] pojedninacniTagovi = tagovi.Split('#');

            List<Tag> tagovi1 = new List<Tag>();

            foreach (string t in pojedninacniTagovi)
            {
                t.Trim();
                t.ToLower();

                if (string.IsNullOrWhiteSpace(t))
                {
                    continue;
                }

                Tag t1 = db.Tagovi.FirstOrDefault(f => f.SadrzajTaga == t);
                if (t1 == null)

                {
                    Tag t2 = new Tag();
                    t2.SadrzajTaga = t;
                    db.Tagovi.Add(t2);
                    tagovi1.Add(t2);
                }
                else
                {
                    if (!listaTagova.Contains(t1))
                    {
                        tagovi1.Add(t1);
                    }
                    else
                    {
                        continue;
                    }                    
                }

            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return 1;
            }

            foreach (var t in tagovi1)
            {
                TagRelacija tr = new TagRelacija();

                tr.SlikaId = SlikaId;
                tr.TagId = t.TagId;
                db.TagRelacije.Add(tr);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return 1;
            }
            return 1;
        }

        [HttpGet]
        [AllowAnonymous]
        public List<Tag> TagoviZaSliku(int? slikaId)
        {
            if (slikaId == null)
            {
                return null;
            }
            List<Tag> listaTagova = new List<Tag>();

            IEnumerable<TagRelacija> listarelacija = db.TagRelacije.Where(tr => tr.SlikaId == slikaId);
            List<int> idTagova = new List<int>();
            foreach (TagRelacija tr in listarelacija)
            {
                idTagova.Add(tr.TagId);                
            }
            foreach (int id in idTagova)
            {
                Tag t = db.Tagovi.Find(id);
                listaTagova.Add(t);
            }
            //List<Tag> lista = listaTagova.Select(t => new { SadrzajTaga = t.SadrzajTaga });
            return listaTagova;
        }

        public bool BrisiTagove(IEnumerable<TagRelacija> relacije)
        {
            foreach (TagRelacija tr in relacije)
            {
                Tag t = db.Tagovi.Find(tr.TagId);
                db.TagRelacije.Remove(tr);
                if (t.TagRelacije.Count == 0)
                {
                    db.Tagovi.Remove(t);
                }
            }

            try
            {

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // GET: Tagovi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tagovi.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tagovi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tagovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagId,SadrzajTaga")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Tagovi.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        // GET: Tagovi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tagovi.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tagovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagId,SadrzajTaga")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: Tagovi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tagovi.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tagovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tagovi.Find(id);
            db.Tagovi.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
