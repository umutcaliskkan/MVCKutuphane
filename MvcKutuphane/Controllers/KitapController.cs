using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities();
        public ActionResult Index(string p)  //Kitapları listelemee için farklı yöntem
        {
            var kitaplar = from k in db.TBLKITAP select k;  //dışardan bir parametre gönderiliyor
            if (!string.IsNullOrEmpty(p))            //eğer gönderilmiş olan parametre boş değilse
            {
                kitaplar = kitaplar.Where(x => x.AD.Contains(p));
            }
           
            return View(kitaplar.ToList());
        }
        [HttpGet]
        public ActionResult KitapEkle()
        {
            //Dropdownlistler için
            List<SelectListItem> dropdown1 = (from i in db.TBLKATEGORI.ToList() //Kategori için list oluşturuldu linq sorguları ile
                                           select new SelectListItem           //"i" kategori tablosunu gezsin
                                           {
                                               Text = i.AD,                  //dropdownlistesi i den gelen ad degerlerine göre dolucak listede yazıcak
                                               Value = i.ID.ToString()       //arkaplanda ise id si çalışıcak
                                           }).ToList();
            ViewBag.drop1 = dropdown1;                                        //Viewbag ile view a taşıyoruz

            List<SelectListItem> dropdown2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.drop2 = dropdown2;
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP p)  //Kitap ekleme işlemi ilişkili tablo olduğu için biraz farklı
        {
            var kategori = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault(); //kategori id değeri çekiliyor
            var yazar = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();          //yazar id değeri çekiliyor
            p.TBLKATEGORI = kategori;                                                            //p ye atandı 
            p.TBLYAZAR = yazar;
            db.TBLKITAP.Add(p);
            db.SaveChanges();                                                                    //kaydedildi
            return RedirectToAction("Index");
            
        }

        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KitapGetir(int id)  //id ye göre getirilecek
        {
            var kitap = db.TBLKITAP.Find(id);     //KlasikDropdown kodları

            List<SelectListItem> dropdown1 = (from i in db.TBLKATEGORI.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.AD,
                                                  Value = i.ID.ToString()
                                              }).ToList();
            ViewBag.drop1 = dropdown1;

            List<SelectListItem> dropdown2 = (from i in db.TBLYAZAR.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.AD + " " + i.SOYAD,
                                                  Value = i.ID.ToString()
                                              }).ToList();
            ViewBag.drop2 = dropdown2;
            return View("KitapGetir", kitap);
        }
        public ActionResult KitapGuncelle(TBLKITAP p)
        {
            var kitap = db.TBLKITAP.Find(p.ID);
            kitap.AD = p.AD;
            kitap.BASIMYIL = p.BASIMYIL;
            kitap.SAYFA = p.SAYFA;
            kitap.YAYINEVI = p.YAYINEVI;
            kitap.DURUM = true;
            var kategori = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yazar = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            kitap.KATEGORI = kategori.ID;
            kitap.YAZAR = yazar.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    
}