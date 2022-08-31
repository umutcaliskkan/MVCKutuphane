using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class islemController : Controller
    {
        // GET: islem
        DBKUTUPHANEMVCEntities db = new DBKUTUPHANEMVCEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.ToList(); //listeleme metodu kullanıldı fakat islemdurumu false olanlar listelenicek
            return View(degerler); //degerler döndürüldü
        }
    }
}