using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Siniflarim;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class VitrinController : Controller
    {
        // GET: Vitrin
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities();
        [HttpGet]
        public ActionResult Index()  /*aynı anda iki tablodan veri çekmek için class oluşturuldu ve vitrin indexine veriler çekiliyor*/
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TBLKITAP.ToList();
            cs.Deger2 = db.TBLHAKKIMIZDA.ToList();
            //var degerler = db.TBLKITAP.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TBLILETISIM t) /*Post işleminde t degiskenine gelen verileri iletisim tablosuna ekleme*/
        {
            db.TBLILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}