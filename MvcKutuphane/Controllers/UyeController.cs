using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBLUYELER.ToList().ToPagedList(sayfa, 3); //Sayfa değeri kaçtan başlasın , sayfada kaçtane listelensin
            return View(degerler); //degerler döndürüldü
        }
        [HttpGet]                 //Sayfa yüklendiğinde bu çalışsın
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]      //Butona basıldığında veya sayfaya bir gönderme işlemi yapılırsa bu çalışsın
        public ActionResult UyeEkle(TBLUYELER p) /*personel tablosundan parametre üretildi*/
        {
            if (!ModelState.IsValid)  //DataAnnotations a bağlı olan gerekçeler sağlanmadıysa bunu yap
            {
                return View("UyeEkle");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return RedirectToAction("/Index"); //p den gelen değerleri tabloya ekle ve sayfayı geri döndür
        }
        public ActionResult UyeSil(int id) //paramde id olucak çünkü id ye göre silme işlemi yapılıyor
        {
            var uye = db.TBLUYELER.Find(id); //tblkategori içerisinde id den gönderdiğim değeri bul
            db.TBLUYELER.Remove(uye); //kategori adlı değişkeni sil
            db.SaveChanges();
            return RedirectToAction("/Index/");
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUYELER.Find(id); //tblkategoride id yi bul
            return View("UyeGetir", uye); //ktg den gelen değerlerleri al ve kategorigetir sayfasına döndür
        }

        public ActionResult UyeGuncelle(TBLUYELER p)  //ekleme gibi olduğu için parametreyi tablodan türettik
        {
            if (!ModelState.IsValid)  //DataAnnotations a bağlı olan gerekçeler sağlanmadıysa bunu yap
            {
                return View("UyeGetir");
            }
            var uye = db.TBLUYELER.Find(p.ID);
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.MAIL = p.MAIL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.SIFRE = p.SIFRE;
            uye.OKUL = p.OKUL;
            uye.TELEFON = p.TELEFON;
            uye.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeKitapGecmis(int id)
        {
            var ktpgcms = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            var uyekit = db.TBLUYELER.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.u1 = uyekit;
            return View(ktpgcms);
        }
    }
}