using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class LaiXeController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();

        // GET: LaiXe
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListLaiXe()
        {
            return Json(new { data = _entities.vt_LaiXe.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string UpdateLaiXe(vt_LaiXe _objlaixe)
        {
            try
            {
                if (_objlaixe.id == 0)
                {
                    vt_LaiXe _new = new vt_LaiXe();
                    _new.maLaiXe = _objlaixe.maLaiXe;
                    _new.tenLaiXe = _objlaixe.tenLaiXe;
                    _new.diaChi = _objlaixe.diaChi;
                    _new.soDienThoai = _objlaixe.soDienThoai;
                    _new.daXoa = 0;
                    _entities.vt_LaiXe.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_LaiXe _update = _entities.vt_LaiXe.Find(_objlaixe.id);
                    _update.maLaiXe = _objlaixe.maLaiXe;
                    _update.tenLaiXe = _objlaixe.tenLaiXe;
                    _update.diaChi = _objlaixe.diaChi;
                    _update.soDienThoai = _objlaixe.soDienThoai;
                    _update.daXoa = 0;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public JsonResult GetLaiXeById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            return Json(_entities.vt_LaiXe.FirstOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idlaixe = int.Parse(Request["idlaixe"]);
                vt_LaiXe _old = _entities.vt_LaiXe.Find(idlaixe);
                if (_old != null)
                {
                    _old.daXoa = 1;
                    _entities.vt_LaiXe.Remove(_old);
                    _entities.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        [HttpPost]
        public bool DeleteByArrayId()
        {
            try
            {
                string idlaixearr = Request["idlaixe"];
                string[] idlaixe = idlaixearr.Split(' ');
                for (int i = 0; i < idlaixe.Length; i++)
                {
                    vt_LaiXe _old = _entities.vt_LaiXe.Find(int.Parse(idlaixe[i]));
                    if (_old != null)
                    {
                        _old.daXoa = 1;
                        _entities.vt_LaiXe.Remove(_old);
                        _entities.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ExistLaiXeMaLaiXe()
        {
            string MaLaiXe = Request.QueryString["MaLaiXe"];
            var chk = _entities.vt_LaiXe.Where(x => x.maLaiXe == MaLaiXe.ToLower() && x.maLaiXe == MaLaiXe.ToUpper() && x.daXoa == 0).FirstOrDefault();
            if (chk != null)
            {
                return false;
            }
            return true;
        }
        public bool ExistLaiXePhone()
        {
            string SoDienThoai = Request.QueryString["SoDienThoai"];
            var chk = _entities.vt_LaiXe.Where(x => x.soDienThoai == SoDienThoai && x.daXoa == 0).FirstOrDefault();
            if (chk != null)
            {
                return false;
            }
            return true;
        }
    }
}