using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;  //kütüphane çağırıldı 

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities(); //database için nesne oluşturuldu
        public ActionResult Index()
        {
            var degerler = db.TBLYAZAR.ToList(); //değişkene ,oluşturulan nesneden tblyazar listelendi
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR p)  //Yazar ekleme işlemleri
        {
            if (!ModelState.IsValid)            //Kontrol yapıldı eğer girilen bilgilerde eksillik varsa yazarekleyi tekrar döndür
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("/Index");
        }

        public ActionResult YazarSil(int id)  //id ye göre silinecek
        {
            var yazar = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarGetir(int id)  //id ye göre getirilecek
        {
            var yazar = db.TBLYAZAR.Find(id);
            return View("YazarGetir", yazar);
        }

        public ActionResult YazarGuncelle(TBLYAZAR p)
        {
            var yazar = db.TBLYAZAR.Find(p.ID);
            yazar.AD = p.AD;
            yazar.SOYAD = p.SOYAD;
            yazar.DETAY = p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TBLKITAP.Where(x => x.YAZAR == id).ToList();
            var yzrad = db.TBLYAZAR.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.y1 = yzrad;
            return View(yazar);
        }
    }
}