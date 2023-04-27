using QLDV.Models;
using QLDV.Models.DAL.GiaoUocService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class GiaoUocThiDuaController : Controller
    {
        // GET: GiaoUocThiDua
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        public GUTDService _gutdsv = new GUTDService();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetGiaoUoc()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            return Json(_gutdsv.getGiaoUocThiDua(), JsonRequestBehavior.AllowGet);
        }
    }
}