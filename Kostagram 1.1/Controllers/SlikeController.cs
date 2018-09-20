using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kostagram_1._1.Models;
using System.IO;
using PagedList;
namespace Kostagram_1._1.Controllers
{
    [Authorize]
    public class SlikeController : Controller
    {

        private AplikacijaDb db = new AplikacijaDb();

        [AllowAnonymous]
        public FileContentResult CitajSliku(int id)
        {
            Slika sl = db.Slike.Find(id);

            if (sl == null)
            {
                return null;
            }
            return File(sl.FajlSlike, sl.TipFajla);
        }

        [AllowAnonymous]
        // GET: Slike
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ApplicationDbContext kontekst = new ApplicationDbContext();
                IEnumerable<string> listaPracenih = kontekst.Pracenja
                    .Where(p => p.ImePratioca == User.Identity.Name)
                    .Take(5).Select(p => p.ImePracenika);
                ViewBag.listaPracenih = listaPracenih.ToList();
            }
            ViewBag.brojSlika = 10;

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult _VratiSlike(int stranica)
        {
            //ApplicationDbContext kontekst = new ApplicationDbContext();
            //IEnumerable<string> listaPracenih = kontekst.Pracenja.
            //    Where(p => p.ImePratioca == User.Identity.Name).Select(p => p.ImePracenika);
            //List<string> lista = new List<string>();
            //foreach (string st in listaPracenih)
            //{
            //    lista.Add(st);
            //}
            //if (!(listaPracenih.Count() == 0))
            //{
            //    IPagedList<Slika> listaSlikaP = db.Slike.Where(s => lista.Contains(s.ImeKorisnika))
            //        .OrderByDescending(s => s.DatumKreiranja).ToPagedList(stranica, 10);
            //    if (listaSlikaP.Count !=0)
            //    {
            //        return PartialView("_VratiSlike", listaSlikaP);
            //    }
                
            //}
                IPagedList<Slika> listaSlika = db.Slike.OrderByDescending(s => s.DatumKreiranja)
                .ToPagedList(stranica, 10);
                return PartialView("_VratiSlike", listaSlika);
                            
        }

        [AllowAnonymous]
        // GET: Slike/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slika slika = db.Slike.Find(id);
            if (slika == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lajkovi = slika.Lajkovi.Count();

            SlikaLajk sl = slika.Lajkovi.SingleOrDefault(l => l.ImeKorisnika == User.Identity.Name);
            if (sl == null)
            {
                ViewBag.Lajkovano = 1;
            }
            else
            {
                ViewBag.Lajkovano = 0;
            }

            ViewBag.ListaTagova = new TagoviController().TagoviZaSliku(id);

            IEnumerable<Komentar> listaKomentara = db.Komentari.Where(k => k.SlikaId == id).ToList();
            ViewBag.komentari = listaKomentara;

            return View(slika);
        }

        // GET: Slike/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slike/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlikaId,ImeKorisnika,FajlSlike,TipFajla,Opis,DatumKreiranja")] Slika slika, HttpPostedFileBase poslatiFajl, string tagovi)
        {
            if (ModelState.IsValid)
            {
                if (poslatiFajl != null)
                {
                    slika.TipFajla = poslatiFajl.ContentType;
                    slika.FajlSlike = new byte[poslatiFajl.ContentLength];
                    Stream st = poslatiFajl.InputStream;
                    st.Read(slika.FajlSlike, 0, poslatiFajl.ContentLength);

                    db.Slike.Add(slika);
                    db.SaveChanges();
                    int SlikaId = slika.SlikaId;

                    int UbacivanjeTagova = new TagoviController().UbaciTagove(SlikaId, tagovi);

                    return RedirectToAction("Index");
                }
                
                
            }

            return View(slika);
        }

        // GET: Slike/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slika slika = db.Slike.Find(id);
            if (slika == null)
            {
                return HttpNotFound();
            }
            string tagovi = "";
            foreach (TagRelacija tr in slika.TagRelacije)
            {
                Tag t = db.Tagovi.Find(tr.TagId);
                tagovi += "#" + t.SadrzajTaga + "";
            }
            ViewBag.tagovi = tagovi;
            return View(slika);
        }

        // POST: Slike/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string Opis, string Tagovi)
        {
            Slika slika = db.Slike.Find(id);

            slika.Opis = Opis;

            int UbacivanjeTagova = new TagoviController().IzmeniTagove(id, Tagovi);
            if (UbacivanjeTagova !=1)
            {
                return View("Error");
            }

            try
            {
                db.Entry(slika).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new {id = slika.SlikaId });
            }
            return RedirectToAction("Details", new { id = slika.SlikaId });
        }

        // GET: Slike/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Slika slika = db.Slike.Find(id);
        //    if (slika == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(slika);
        //}

        // POST: Slike/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Slika slika = db.Slike.Find(id);
            db.Slike.Remove(slika);
            foreach (Komentar k in slika.Komentari)
            {
                db.Komentari.Remove(k);
            }
            foreach (SlikaLajk lajk in slika.Lajkovi)
            {
                db.SlikaLajkovi.Remove(lajk);
            }
            IEnumerable<TagRelacija> listaTagova = slika.TagRelacije;
            if (new TagoviController().BrisiTagove(listaTagova))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                foreach (TagRelacija tr in listaTagova)
                {
                    db.TagRelacije.Remove(tr);
                }
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    return RedirectToAction("Error");
                }
            }
        }

        //[ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult _ProfilSlike(string ime)
        {

            IEnumerable<Slika> listaSlika = db.Slike.Where(s => s.ImeKorisnika == ime)
                .ToList().OrderByDescending(s => s.DatumKreiranja);
           
            return PartialView("_ProfilSlike", listaSlika.ToList());
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
