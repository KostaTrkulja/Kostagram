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
    public class PrivatnePorukeController : Controller
    {
        private AplikacijaDb db = new AplikacijaDb();

        // GET: PrivatnePoruke
        public ActionResult Inbox(string ImePrimaoca=null)
        {            
            ViewBag.Ime = ImePrimaoca;
            
            return View();
        }

        public PartialViewResult _NovaPoruka()
        {
            //PrivatnaPoruka pp = new PrivatnaPoruka();

            //pp.ImePosiljaoca = User.Identity.Name;            
            //pp.Poslata = false;
            //pp.Procitana = false;

            //try
            //{
            //    db.PrivatnePoruke.Add(pp);
            //    db.SaveChanges();
            //}
            //catch (Exception)
            //{
            //    new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //}
            return PartialView("_NovaPoruka");
        }

        public PartialViewResult _PrimljenePoruke()
        {
            IEnumerable<PrivatnaPoruka> listaPoruka = db.PrivatnePoruke
                .Where(p => p.ImePrimaoca == User.Identity.Name)
                .Where(p => p.Poslata == true)
                .OrderByDescending(p => p.DatumKreiranja);

            return PartialView("_PrimljenePoruke", listaPoruka);
        }

        public PartialViewResult _PoslatePoruke()
        {
            IEnumerable<PrivatnaPoruka> listaPoruka = db.PrivatnePoruke
                .Where(p => p.ImePosiljaoca == User.Identity.Name)
                .Where(p => p.Poslata == true)
                .OrderByDescending(p => p.DatumKreiranja);

            return PartialView("_PoslatePoruke", listaPoruka);
        }

        public int VratiBroj()
        {
            int broj = db.PrivatnePoruke
                .Where(p => p.ImePrimaoca == User.Identity.Name)
                .Where(p=>p.Poslata == true)
                .Where(pp => pp.Procitana == false).Count();
            return broj;
        }

        public PartialViewResult _PorukaDetalji(int id)
        {
            PrivatnaPoruka poruka = db.PrivatnePoruke.Find(id);

            poruka.Procitana = true;
            try
            {
                db.Entry(poruka).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return PartialView("_PorukaDetalji", poruka);
            }
            
            return PartialView("_PorukaDetalji", poruka);
        }

        // GET: PrivatnePoruke/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivatnaPoruka privatnaPoruka = db.PrivatnePoruke.Find(id);
            if (privatnaPoruka == null)
            {
                return HttpNotFound();
            }
            return View(privatnaPoruka);
        }

        // GET: PrivatnePoruke/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrivatnePoruke/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrivatnaPorukaId,ImePosiljaoca,ImePrimaoca,Naslov,Sadrzaj,DatumKreiranja")] PrivatnaPoruka privatnaPoruka)
        {
            privatnaPoruka.Poslata = true;
            privatnaPoruka.Procitana = false;
            if (ModelState.IsValid)
            {

                db.PrivatnePoruke.Add(privatnaPoruka);
                db.SaveChanges();
                return RedirectToAction("Inbox");
            }

            return View(privatnaPoruka);
        }

        // GET: PrivatnePoruke/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivatnaPoruka privatnaPoruka = db.PrivatnePoruke.Find(id);
            if (privatnaPoruka == null)
            {
                return HttpNotFound();
            }
            return View(privatnaPoruka);
        }

        // POST: PrivatnePoruke/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrivatnaPorukaId,ImePosiljaoca,ImePrimaoca,Naslov,Sadrzaj,DatumKreiranja")] PrivatnaPoruka privatnaPoruka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(privatnaPoruka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(privatnaPoruka);
        }

        // GET: PrivatnePoruke/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivatnaPoruka privatnaPoruka = db.PrivatnePoruke.Find(id);
            if (privatnaPoruka == null)
            {
                return HttpNotFound();
            }
            return View(privatnaPoruka);
        }

        // POST: PrivatnePoruke/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrivatnaPoruka privatnaPoruka = db.PrivatnePoruke.Find(id);
            db.PrivatnePoruke.Remove(privatnaPoruka);
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
