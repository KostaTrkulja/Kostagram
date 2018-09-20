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
    public class SlikaLajkoviController : Controller
    {
        private AplikacijaDb db = new AplikacijaDb();

        [HttpGet]
        [AllowAnonymous]
        public JsonResult VratiLajkove(int id)
        {
            Slika s = db.Slike.Find(id);

            int lajkovano = 1;
            SlikaLajk sl = s.Lajkovi.SingleOrDefault(l => l.ImeKorisnika == User.Identity.Name);
            if (sl == null)
            {
                lajkovano = 0;
            }
            var brojLajkova = new { brojLajkova = s.Lajkovi.Count(), lajkovano = lajkovano };
            return Json(brojLajkova, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Lajkuj(int id)
        {
            Slika slika = db.Slike.Find(id);

            SlikaLajk s = slika.Lajkovi.SingleOrDefault(l => l.ImeKorisnika == User.Identity.Name);
            if (s != null)
            {
                return slika.Lajkovi.Count();
            }

            SlikaLajk sl = new SlikaLajk();
            sl.SlikaId = id;
            sl.ImeKorisnika = User.Identity.Name;
            try
            {
                db.SlikaLajkovi.Add(sl);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return slika.Lajkovi.Count();
            }
            return slika.Lajkovi.Count();
        }

        [HttpPost]
        public int ObrisiLajk(int id)
        {
            Slika slika = db.Slike.Find(id);
            SlikaLajk sl = slika.Lajkovi.SingleOrDefault(l => l.ImeKorisnika == User.Identity.Name);

            try
            {
                db.SlikaLajkovi.Remove(sl);
                db.SaveChanges();
                return slika.Lajkovi.Count();
            }
            catch (Exception)
            {

                return slika.Lajkovi.Count();
            }
        }

        [AllowAnonymous]
        public PartialViewResult _ProfilSlikaLajkovi(string ime)
        {

            IEnumerable<SlikaLajk> lajkovi = db.SlikaLajkovi.Where(l => l.ImeKorisnika == ime);

            List<SlikaLajk> listaLajkova = new List<SlikaLajk>();

            foreach (SlikaLajk lajk in lajkovi)
            {
                listaLajkova.Add(lajk);
            }
            List<Slika> listaSlika = new List<Slika>();

            foreach (SlikaLajk lajk in listaLajkova)
            {
                Slika sl = db.Slike.Find(lajk.SlikaId);
                listaSlika.Add(sl);
            }

            return PartialView("_ProfilSlikaLajkovi", listaSlika);
        }


        // GET: SlikaLajkovi
        public ActionResult Index()
        {
            var slikaLajkovi = db.SlikaLajkovi.Include(s => s.Slika);
            return View(slikaLajkovi.ToList());
        }

        // GET: SlikaLajkovi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlikaLajk slikaLajk = db.SlikaLajkovi.Find(id);
            if (slikaLajk == null)
            {
                return HttpNotFound();
            }
            return View(slikaLajk);
        }

        // GET: SlikaLajkovi/Create
        public ActionResult Create()
        {
            ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika");
            return View();
        }

        // POST: SlikaLajkovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlikaLajkId,SlikaId,ImeKorisnika")] SlikaLajk slikaLajk)
        {
            if (ModelState.IsValid)
            {
                db.SlikaLajkovi.Add(slikaLajk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika", slikaLajk.SlikaId);
            return View(slikaLajk);
        }

        // GET: SlikaLajkovi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlikaLajk slikaLajk = db.SlikaLajkovi.Find(id);
            if (slikaLajk == null)
            {
                return HttpNotFound();
            }
            ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika", slikaLajk.SlikaId);
            return View(slikaLajk);
        }

        // POST: SlikaLajkovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlikaLajkId,SlikaId,ImeKorisnika")] SlikaLajk slikaLajk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slikaLajk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SlikaId = new SelectList(db.Slike, "SlikaId", "ImeKorisnika", slikaLajk.SlikaId);
            return View(slikaLajk);
        }

        // GET: SlikaLajkovi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlikaLajk slikaLajk = db.SlikaLajkovi.Find(id);
            if (slikaLajk == null)
            {
                return HttpNotFound();
            }
            return View(slikaLajk);
        }

        // POST: SlikaLajkovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SlikaLajk slikaLajk = db.SlikaLajkovi.Find(id);
            db.SlikaLajkovi.Remove(slikaLajk);
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
