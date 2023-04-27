using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{

    public class KhachHangController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();

        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListKhachHang()
        {
            return Json(new { data = _entities.vt_DMKhachHang.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string UpdateKhachHang(vt_DMKhachHang _objKH)
        {
            try
            {
                if (_objKH.id == 0)
                {
                    vt_DMKhachHang _new = new vt_DMKhachHang();
                    _new.maKhachHang = _objKH.maKhachHang;
                    _new.tenKhachHang = _objKH.tenKhachHang;
                    _new.loaiKhachHang = _objKH.loaiKhachHang;
                    _new.diaChi = _objKH.diaChi;
                    _new.soDienThoai = _objKH.soDienThoai;
                    _new.daXoa = 0;
                    _entities.vt_DMKhachHang.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_DMKhachHang _update = _entities.vt_DMKhachHang.Find(_objKH.id);
                    _update.maKhachHang = _objKH.maKhachHang;
                    _update.tenKhachHang = _objKH.tenKhachHang;
                    _update.loaiKhachHang = _objKH.loaiKhachHang;
                    _update.diaChi = _objKH.diaChi;
                    _update.soDienThoai = _objKH.soDienThoai;
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
        public JsonResult GetKhachHangById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            return Json(_entities.vt_DMKhachHang.FirstOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idkhachhang = int.Parse(Request["idkhachhang"]);
                vt_DMKhachHang _old = _entities.vt_DMKhachHang.Find(idkhachhang);
                if (_old != null)
                {
                    _old.daXoa = 1;
                    _entities.vt_DMKhachHang.Remove(_old);
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
                string idkhachhangarr = Request["idkhachhang"];
                string[] idkhachhang = idkhachhangarr.Split(' ');
                for (int i = 0; i < idkhachhang.Length; i++)
                {
                    vt_DMKhachHang _old = _entities.vt_DMKhachHang.Find(int.Parse(idkhachhang[i]));
                    if (_old != null)
                    {
                        _old.daXoa = 1;
                        _entities.vt_DMKhachHang.Remove(_old);
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

        public bool ExistKhachHangMaKH()
        {
            string MaKhachHang = Request.QueryString["MaKhachHang"];
            var chk = _entities.vt_DMKhachHang.Where(x => x.maKhachHang == MaKhachHang.ToLower() && x.maKhachHang == MaKhachHang.ToUpper() && x.daXoa == 0).FirstOrDefault();
            if (chk != null)
            {
                return false;
            }
            return true;
        }
        public bool ExistKhachHangPhone()
        {
            string SoDienThoai = Request.QueryString["SoDienThoai"];
            var chk = _entities.vt_DMKhachHang.Where(x => x.soDienThoai == SoDienThoai && x.daXoa == 0).FirstOrDefault();
            if (chk != null)
            {
                return false;
            }
            return true;
        }
    }
}