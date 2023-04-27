using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class TinhThanhController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        // GET: TinhThanh
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListTinhThanh()
        {
            return Json(_entities.TinhThanhs.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}