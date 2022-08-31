using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList(); //listeleme metodu kullanıldı fakat islemdurumu false olanlar listelenicek
            return View(degerler); //degerler döndürüldü
        }
        [HttpGet]    //Sayfa yüklendiğinde bu çalışsın
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from x in db.TBLUYELER.ToList() //odunc verme indexine üyeleri dropdownliste getirmek için işlemler
                                           select new SelectListItem
                                           {
                                               Text = x.AD + " " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from y in db.TBLKITAP.Where(x=>x.DURUM==true).ToList() //odunc verme indexine kitapları dropdownliste getirmek için işlemler
                                           select new SelectListItem
                                           {
                                               Text = y.AD,
                                               Value = y.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from z in db.TBLPERSONEL.ToList() //odunc verme indexine kitapları dropdownliste getirmek için işlemler
                                           select new SelectListItem
                                           {
                                               Text = z.PERSONEL,
                                               Value = z.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]      //Butona basıldığında veya sayfaya bir gönderme işlemi yapılırsa bu çalışsın
        public ActionResult OduncVer(TBLHAREKET p) /*kategori tablosundan parametre üretildi*/
        {
            var d1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var d2 = db.TBLKITAP.Where(y => y.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(z => z.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            p.TBLUYELER = d1;
            p.TBLKITAP = d2;
            p.TBLPERSONEL = d3;
            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return RedirectToAction("/Index"); //p den gelen değerleri tabloya ekle ve sayfayı geri döndür
        }

        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odunc = db.TBLHAREKET.Find(p.ID); //tblkategoride id yi bul
            DateTime d1 = DateTime.Parse(odunc.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;

            return View("Odunciade", odunc); //ktg den gelen değerlerleri al ve kategorigetir sayfasına döndür
        }

        public ActionResult OduncGuncelle(TBLHAREKET p)  //ekleme gibi olduğu için parametreyi tablodan türettik
        {
            if (!ModelState.IsValid)  //DataAnnotations a bağlı olan gerekçeler sağlanmadıysa bunu yap
            {
                return View("Index");
            }
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}