using QLDV.Models;
using QLDV.Models.DAL.LichTrinhService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class LichTrinhXeController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private LichTrinhService _lichtrinhService = new LichTrinhService();

        // GET: LichTrinhXe
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListLichTrinhXe()
        {
            return Json(new { data = _lichtrinhService.getDanhSachLichTrinh() }, JsonRequestBehavior.AllowGet);

        }
        public void CapnhatMaChuyen(int id)
        {
            try
            {
                vt_LichTrinhXe _update = _entities.vt_LichTrinhXe.Find(id);

                _update.maChuyen = "MC" + id;
                _entities.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        [HttpPost]
        public string UpdateLichTrinhXe(vt_LichTrinhXe _objLTX)
        {
            vt_LaiXe laiXe = _entities.vt_LaiXe.FirstOrDefault();
            vt_DMKhachHang khachhang = _entities.vt_DMKhachHang.FirstOrDefault();
            try
            {
                if (_objLTX.id == 0)
                {
                    vt_LichTrinhXe _new = new vt_LichTrinhXe();
                    _new.maChuyen = "MC"+_objLTX.id;
                    _new.idLaiXe = laiXe.id;
                    _new.idKhachHang = khachhang.id;
                    _new.thoiGianLapPhieu = _objLTX.thoiGianLapPhieu;
                    _new.noiDungLamViec = _objLTX.noiDungLamViec;
                    _new.noiDi = _objLTX.noiDi;
                    _new.noiDen = _objLTX.noiDen;
                    _new.giaChuaThue = _objLTX.giaChuaThue;
                    _new.thueVat = _objLTX.thueVat;
                    _new.tongThanhToan = _objLTX.tongThanhToan;
                    _new.tinhTrangThanhToan = _objLTX.tinhTrangThanhToan;
                    _new.checkPhieu = _objLTX.checkPhieu;
                    _new.daXoa = 0;
                    _entities.vt_LichTrinhXe.Add(_new);
                    _entities.SaveChanges();
                    CapnhatMaChuyen(_new.id);
                    return "addsuccess";
                }
                else
                {
                    vt_LichTrinhXe _update = _entities.vt_LichTrinhXe.Find(_objLTX.id);
                    _update.maChuyen = "MC" + _objLTX.id;
                    _update.idLaiXe = laiXe.id;
                    _update.idKhachHang = khachhang.id;
                    _update.thoiGianLapPhieu = _objLTX.thoiGianLapPhieu;
                    _update.noiDungLamViec = _objLTX.noiDungLamViec;
                    _update.noiDi = _objLTX.noiDi;
                    _update.noiDen = _objLTX.noiDen;
                    _update.giaChuaThue = _objLTX.giaChuaThue;
                    _update.thueVat = _objLTX.thueVat;
                    _update.tongThanhToan = _objLTX.tongThanhToan;
                    _update.tinhTrangThanhToan = _objLTX.tinhTrangThanhToan;
                    _update.checkPhieu = _objLTX.checkPhieu;
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
        public JsonResult GetLichTrinhXeById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _lichtrinhService.GetLichTrinhXeById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
/*            return Json(_entities.vt_LichTrinhXe.FirstOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
*/        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idlichtrinhxe = int.Parse(Request["idlichtrinhxe"]);
                vt_LichTrinhXe _old = _entities.vt_LichTrinhXe.Find(idlichtrinhxe);
                if (_old != null)
                {
                    _old.daXoa = 1;
                    _entities.vt_LichTrinhXe.Remove(_old);
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
                string idlichtrinhxearr = Request["idlichtrinhxe"];
                string[] idlichtrinhxe = idlichtrinhxearr.Split(' ');
                for (int i = 0; i < idlichtrinhxe.Length; i++)
                {
                    vt_LichTrinhXe _old = _entities.vt_LichTrinhXe.Find(int.Parse(idlichtrinhxe[i]));
                    if (_old != null)
                    {
                        _old.daXoa = 1;
                        _entities.vt_LichTrinhXe.Remove(_old);
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

        public bool ExistLichTrinhMaChuyen()
        {
            string MaChuyen = Request.QueryString["MaChuyen"];
            var chk = _entities.vt_LichTrinhXe.Where(x => x.maChuyen == MaChuyen.ToLower() && x.maChuyen == MaChuyen.ToUpper() && x.daXoa == 0).FirstOrDefault();
            if (chk != null)
            {
                return false;
            }
            return true;
        }
        public JsonResult GetDmLaiXe()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            var data = _entities.vt_LaiXe.Where(x => x.daXoa == 0).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDmKhachHang()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            var data = _entities.vt_DMKhachHang.Where(x => x.daXoa == 0).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
           
        }
    }
}