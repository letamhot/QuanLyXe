using QLDV.Models;
using QLDV.Models.DAL.ChiPhiSuaXe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class ChiPhiSuaChuaXeController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private ChiPhiSuaXeService _chiphisuachuaService = new ChiPhiSuaXeService();

        // GET: ChiPhiSuaChuaXe
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListChiPhiSuaXe()
        {
            return Json(new { data = _chiphisuachuaService.GetListChiPhiSuaXe() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string UpdateChiPhiSuaXe(vt_ChiPhiSuaXe _obj)
        {
            try
            {
                if (_obj.id == 0)
                {
                    vt_ChiPhiSuaXe _new = new vt_ChiPhiSuaXe();
                    _new.ngayNhap = _obj.ngayNhap;
                    _new.vatTu = _obj.vatTu;
                    _new.soLuong = _obj.soLuong;
                    _new.donViTinh = _obj.donViTinh;
                    _new.donGia = _obj.donGia;
                    _new.thanhTien = _obj.thanhTien;
                    _new.ghiChu = _obj.ghiChu;
                    _entities.vt_ChiPhiSuaXe.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_ChiPhiSuaXe _update = _entities.vt_ChiPhiSuaXe.Find(_obj.id);
                    _update.ngayNhap = _obj.ngayNhap;
                    _update.vatTu = _obj.vatTu;
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

        public JsonResult GetChiPhiSuaXeById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _chiphisuachuaService.GetChiPhiSuaXeById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
           
        }
        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idchiphisuaxe = int.Parse(Request["idchiphisuaxe"]);
                vt_ChiPhiSuaXe _old = _entities.vt_ChiPhiSuaXe.Find(idchiphisuaxe);
                if (_old != null)
                {
                    _entities.vt_ChiPhiSuaXe.Remove(_old);
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
                string idchiphisuaxearr = Request["idchiphisuaxe"];
                string[] idchiphisuaxe = idchiphisuaxearr.Split(' ');
                for (int i = 0; i < idchiphisuaxe.Length; i++)
                {
                    vt_ChiPhiSuaXe _old = _entities.vt_ChiPhiSuaXe.Find(int.Parse(idchiphisuaxe[i]));
                    if (_old != null)
                    {
                        _entities.vt_ChiPhiSuaXe.Remove(_old);
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