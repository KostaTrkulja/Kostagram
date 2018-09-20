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
    public class KomentariController : Controller
    {
        private AplikacijaDb db = new AplikacijaDb();

        [AllowAnonymous]
        public PartialViewResult _VratiKomentare(int id)
        {
            IEnumerable<Komentar> listaKomentara = db.Komentari.Where(k => k.SlikaId == id).ToList().
                OrderByDescending(k => k.DatumKreiranja);

            ViewBag.slikaId = id;
            return PartialView("_VratiKomentare", listaKomentara.ToList());
        }

        [HttpPost]
        public int ObrisiKomentar(int KomentarId)
        {
            Komentar komentar = db.Komentari.Find(KomentarId);

            try
            {
                db.Komentari.Remove(komentar);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        // GET: Komentari
        public ActionResult Index()
        {
            var komentari = db.Komentari.Include(k => k.Slika);
            return View(komentari.ToList());
        }

        // GET: Komentari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }

        [HttpGet]
        // GET: Komentari/Create
        public ActionResult Create()
        {
            ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public int UbaciKomentar(int SlikaId, string TeloKomentara)
        {
            Komentar k = new Komentar();
            k.SlikaId = SlikaId;
            k.DatumKreiranja = DateTime.Now;
            k.ImeKorisnika = User.Identity.Name;
            k.TeloKomentara = TeloKomentara;

            try
            {
                db.Komentari.Add(k);
                db.SaveChanges();
                return 1;                
            }
            catch (Exception)
            {

                return 0;
            }


            
        }

        // POST: Komentari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost]
        public int Create(int SlikaId, string TeloKomentara)
        {
            Komentar k = new Komentar();
            k.SlikaId = SlikaId;
            k.DatumKreiranja = DateTime.Now;
            k.ImeKorisnika = User.Identity.Name;
            k.TeloKomentara = TeloKomentara;

            try
            {
                db.Komentari.Add(k);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

            
        }

        //public ActionResult Create([Bind(Include = "KomentarId,SlikaId,TeloKomentara,DatumKreiranja,ImeKorisnika")] Komentar komentar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Komentari.Add(komentar);
        //        db.SaveChanges();
        //        return RedirectToAction("/Slike/Index");
        //    }

        //    ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika", komentar.SlikaId);
        //    return RedirectToAction("/Slike/Index");
        //}

        // GET: Komentari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika", komentar.SlikaId);
            return View(komentar);
        }

        // POST: Komentari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KomentarId,SlikaId,TeloKomentara,DatumKreiranja,ImeKorisnika")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komentar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika", komentar.SlikaId);
            return View(komentar);
        }

        // GET: Komentari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }

        // POST: Komentari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Komentar komentar = db.Komentari.Find(id);
            db.Komentari.Remove(komentar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public PartialViewResult _ProfilKomentari(string ime)
        {
            IEnumerable<Komentar> listaKomentara = db.Komentari.Where(k => k.ImeKorisnika == ime)
                .ToList().OrderByDescending(k => k.DatumKreiranja);

            return PartialView("_ProfilKomentari", listaKomentara.ToList());
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
