﻿using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDV.Models.DAL.DangKiemGiayToXe;


namespace QLDV.Controllers
{
    public class DangKiemGiayToXeController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private DangKiemGiayToXeService _dangkiemService = new DangKiemGiayToXeService();
        // GET: DangKiemGiayToXe
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDangKiem()
        {
            return Json(new { data = _dangkiemService.GetDangKiem() }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetDangKiemById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _dangkiemService.GetDangKiemById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
            /*            return Json(_entities.vt_LichTrinhXe.FirstOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
            */
        }
        public JsonResult GetXeVanTai()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            var data = _entities.vt_XeVanTai.Where(x => x.daXoa == "0").ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string UpdateDangKiemXe(vt_DangKiemGiayToXe _objDangKiem)
        {
            vt_XeVanTai xeVanTai = _entities.vt_XeVanTai.FirstOrDefault();
            try
            {
                if (_objDangKiem.id == 0)
                {
                    vt_DangKiemGiayToXe _new = new vt_DangKiemGiayToXe();
                    _new.id = _objDangKiem.id;
                    _new.idXeVanTai = _objDangKiem.idXeVanTai;
                    _new.kiemDinhSo = _objDangKiem.kiemDinhSo;
                    _new.ngayKiemDinh = _objDangKiem.ngayKiemDinh;
                    _new.loaiKiemDinh = _objDangKiem.loaiKiemDinh;
                    _new.noiDung = _objDangKiem.noiDung;
                    _new.ngayHetHan = _objDangKiem.ngayHetHan;
                    _new.ghiChu = _objDangKiem.ghiChu;
                    _new.daXoa = "0";
                    _entities.vt_DangKiemGiayToXe.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_DangKiemGiayToXe _update = _entities.vt_DangKiemGiayToXe.Find(_objDangKiem.id);
                    _update.id = _objDangKiem.id;
                    _update.idXeVanTai = _objDangKiem.idXeVanTai;
                    _update.kiemDinhSo = _objDangKiem.kiemDinhSo;
                    _update.ngayKiemDinh = _objDangKiem.ngayKiemDinh;
                    _update.loaiKiemDinh = _objDangKiem.loaiKiemDinh;
                    _update.noiDung = _objDangKiem.noiDung;

                    _update.ngayHetHan = _objDangKiem.ngayHetHan;
                    _update.ghiChu = _objDangKiem.ghiChu;
                    _update.daXoa = "0";
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int iddangkiem = int.Parse(Request["iddangkiem"]);
                vt_DangKiemGiayToXe _old = _entities.vt_DangKiemGiayToXe.Find(iddangkiem);
                if (_old != null)
                {
                    _old.daXoa = "1";
                    _entities.vt_DangKiemGiayToXe.Remove(_old);
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
                string iddangkiemarr = Request["iddangkiem"];
                string[] iddangkiem = iddangkiemarr.Split(' ');
                for (int i = 0; i < iddangkiem.Length; i++)
                {
                    vt_DangKiemGiayToXe _old = _entities.vt_DangKiemGiayToXe.Find(int.Parse(iddangkiem[i]));

                    if (_old != null)
                    {
                        _old.daXoa = "1";
                        _entities.vt_DangKiemGiayToXe.Remove(_old);
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


    }
}