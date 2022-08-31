using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity; /* Veriler için kütüphane tanımlandı*/

namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities(); /*db modelinden nesne oluşturduk*/
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORI.Where(x=>x.DURUM==true).ToList(); //listeleme metodu kullanıldı
            return View(degerler); //degerler döndürüldü
        }
        [HttpGet]                 //Sayfa yüklendiğinde bu çalışsın
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]      //Butona basıldığında veya sayfaya bir gönderme işlemi yapılırsa bu çalışsın
        public ActionResult KategoriEkle(TBLKATEGORI p) /*kategori tablosundan parametre üretildi*/
        {
            db.TBLKATEGORI.Add(p);
            db.SaveChanges();
            return RedirectToAction("/Index"); //p den gelen değerleri tabloya ekle ve sayfayı geri döndür
        }

        public ActionResult KategoriSil(int id) //paramde id olucak çünkü id ye göre silme işlemi yapılıyor
        {
            var kategori = db.TBLKATEGORI.Find(id); //tblkategori içerisinde id den gönderdiğim değeri bul
                                                    //  db.TBLKATEGORI.Remove(kategori); //kategori adlı değişkeni sil
            kategori.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("/Index/");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id); //tblkategoride id yi bul
            return View("KategoriGetir", ktg); //ktg den gelen değerlerleri al ve kategorigetir sayfasına döndür
        }
            
        public ActionResult KategoriGuncelle(TBLKATEGORI p)  //ekleme gibi olduğu için parametreyi tablodan türettik
        {
            var ktg = db.TBLKATEGORI.Find(p.ID);
            ktg.AD = p.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}