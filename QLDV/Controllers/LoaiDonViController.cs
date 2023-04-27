using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class LoaiDonViController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        // GET: LoaiDonVi
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListLoaiDonVi()
        {
            return Json(_entities.LoaiDonVis.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}