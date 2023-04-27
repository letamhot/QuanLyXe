using QLDV.Models;
using QLDV.Models.DAL.ChiPhiKhac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class ChiPhiKhacController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private ChiPhiKhacService _chiphikhacService = new ChiPhiKhacService();

        // GET: ChiPhiKhac
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListChiPhiKhac()
        {
            return Json(new { data = _chiphikhacService.GetListChiPhiKhac() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string UpdateChiPhiKhac(vt_ChiPhiKhac _obj)
        {
            try
            {
                if (_obj.id == 0)
                {
                    vt_ChiPhiKhac _new = new vt_ChiPhiKhac();
                    _new.tenChiPhi = _obj.tenChiPhi;
                    _new.ngayThanhToan = _obj.ngayThanhToan;
                    _new.noiDung = _obj.noiDung;
                    _new.soLuong = _obj.soLuong;
                    _new.donViTinh = _obj.donViTinh;
                    _new.donGia = _obj.donGia;
                    _new.thanhTien = _obj.thanhTien;
                    _new.ghiChu = _obj.ghiChu;
                    _entities.vt_ChiPhiKhac.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_ChiPhiKhac _update = _entities.vt_ChiPhiKhac.Find(_obj.id);
                    _update.tenChiPhi = _obj.tenChiPhi;

                    _update.ngayThanhToan = _obj.ngayThanhToan;
                    _update.noiDung = _obj.noiDung;
                    _update.soLuong = _obj.soLuong;
                    _update.donViTinh = _obj.donViTinh;
                    _update.donGia = _obj.donGia;
                    _update.thanhTien = _obj.thanhTien;
                    _update.ghiChu = _obj.ghiChu;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public JsonResult GetChiPhiKhacById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _chiphikhacService.GetChiPhiKhacById(id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idchiphikhac = int.Parse(Request["idchiphikhac"]);
                vt_ChiPhiKhac _old = _entities.vt_ChiPhiKhac.Find(idchiphikhac);
                if (_old != null)
                {
                    _entities.vt_ChiPhiKhac.Remove(_old);
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
                string idchiphikhacarr = Request["idchiphikhac"];
                string[] idchiphikhac = idchiphikhacarr.Split(' ');
                for (int i = 0; i < idchiphikhac.Length; i++)
                {
                    vt_ChiPhiKhac _old = _entities.vt_ChiPhiKhac.Find(int.Parse(idchiphikhac[i]));
                    if (_old != null)
                    {
                        _entities.vt_ChiPhiKhac.Remove(_old);
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