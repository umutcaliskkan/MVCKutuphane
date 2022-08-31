using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLPERSONEL.ToList(); //listeleme metodu kullanıldı
            return View(degerler); //degerler döndürüldü
        }
        [HttpGet]                 //Sayfa yüklendiğinde bu çalışsın
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]      //Butona basıldığında veya sayfaya bir gönderme işlemi yapılırsa bu çalışsın
        public ActionResult PersonelEkle(TBLPERSONEL p) /*personel tablosundan parametre üretildi*/
        {
            if (!ModelState.IsValid)  //DataAnnotations a bağlı olan gerekçeler sağlanmadıysa bunu yap
            {
                return View("PersonelEkle");
            }
            db.TBLPERSONEL.Add(p);
            db.SaveChanges();
            return RedirectToAction("/Index"); //p den gelen değerleri tabloya ekle ve sayfayı geri döndür
        }

        public ActionResult PersonelSil(int id) //paramde id olucak çünkü id ye göre silme işlemi yapılıyor
        {
            var personel = db.TBLPERSONEL.Find(id); //tblkategori içerisinde id den gönderdiğim değeri bul
            db.TBLPERSONEL.Remove(personel); //kategori adlı değişkeni sil
            db.SaveChanges();
            return RedirectToAction("/Index/");
        }

        public ActionResult PersonelGetir(int id)
        {
            var pers = db.TBLPERSONEL.Find(id); //tblkategoride id yi bul
            return View("PersonelGetir", pers); //ktg den gelen değerlerleri al ve kategorigetir sayfasına döndür
        }

        public ActionResult PersonelGuncelle(TBLPERSONEL p)  //ekleme gibi olduğu için parametreyi tablodan türettik
        {
            if (!ModelState.IsValid)  //DataAnnotations a bağlı olan gerekçeler sağlanmadıysa bunu yap
            {
                return View("PersonelGetir");
            }
            var pers = db.TBLPERSONEL.Find(p.ID);
            pers.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}