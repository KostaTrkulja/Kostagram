using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kostagram_1._1.Models;

namespace Kostagram_1._1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AplikacijaDb db = new AplikacijaDb();

            TimeSpan brojDana = DateTime.Today - new DateTime(2000, 1, 1);
            int dan = (int)brojDana.TotalDays;
            Random rnd = new Random(dan);
            int id;
            Slika sl = null;
            do
            {
                id = rnd.Next(1, db.Slike.Count());
                sl = db.Slike.Find(id);
            } while (sl ==null);

            ViewBag.slika = sl;

            ViewBag.tagovi = db.Tagovi.OrderByDescending(t => t.TagRelacije.Count())
                .Take(10).Select(t => t.SadrzajTaga).ToList();
            return View(sl);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}