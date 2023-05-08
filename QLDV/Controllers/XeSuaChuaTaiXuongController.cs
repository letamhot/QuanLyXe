using QLDV.Models;
using QLDV.Models.DAL.XeSuaChuaTaiXuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class XeSuaChuaTaiXuongController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private  XeSuaChuaTaiXuongService _xeSuaChuaTaiXuongService = new XeSuaChuaTaiXuongService();
        // GET: XeSuaChuaTaiXuong
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getDanhSachXeSuaChuaTaiXuong()
        {
            return Json(new { data = _xeSuaChuaTaiXuongService.getDanhSachXeSuaChuaTaiXuong() }, JsonRequestBehavior.AllowGet);

        }
        public string UpdateXeSuaChua(vt_XeSuaChuaTaiXuong _obj)
        {
            vt_XeVanTai xevantai = _entities.vt_XeVanTai.FirstOrDefault();
            vt_DMKhachHang khachhang = _entities.vt_DMKhachHang.FirstOrDefault();
            try
            {
                if (_obj.id == 0)
                {
                    vt_XeSuaChuaTaiXuong _new = new vt_XeSuaChuaTaiXuong();
                   
                    _new.ngayThang = _obj.ngayThang;
                    _new.idXeVanTai = xevantai.id;
                    _new.idKhachHang = khachhang.id;
                    if (_obj.noiDungSuaChua == null || _obj.noiDungSuaChua == "")
                    {
                        _new.noiDungSuaChua = null;

                    }
                    else
                    {
                        _new.noiDungSuaChua = _obj.noiDungSuaChua;
                    }
                    if (_obj.soLuong == null)
                    {
                        _new.soLuong = null;

                    }
                    else
                    {
                        _new.soLuong = _obj.soLuong;
                    }
                    if (_obj.donViTinh == null || _obj.donViTinh == "")
                    {
                        _new.donViTinh = null;

                    }
                    else
                    {
                        _new.donViTinh = _obj.donViTinh;
                    }
                    if (_obj.donGia == null)
                    {
                        _new.donGia = null;

                    }
                    else
                    {
                        _new.donGia = _obj.donGia;
                    }
                    _new.thanhTienChuaThue = _obj.thanhTienChuaThue;
                    _new.thueVat = _obj.thueVat;
                    _new.tongThanhToan = _obj.tongThanhToan;
                    if (_obj.no == null)
                    {
                        _new.no = 0;

                    }
                    else
                    {
                        _new.no = _obj.no;
                    }
                    if (_obj.canTru == null)
                    {
                        _new.canTru = 0;

                    }
                    else
                    {
                        _new.canTru = _obj.canTru;
                    }
                    if (_obj.ghiChu == null)
                    {
                        _new.ghiChu = null;

                    }
                    else
                    {
                        _new.ghiChu = _obj.ghiChu;
                    }
                    _entities.vt_XeSuaChuaTaiXuong.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_XeSuaChuaTaiXuong _update = _entities.vt_XeSuaChuaTaiXuong.Find(_obj.id);
                    _update.ngayThang = _obj.ngayThang;
                    _update.idXeVanTai = xevantai.id;
                    _update.idKhachHang = khachhang.id;
                    if (_obj.noiDungSuaChua == null || _obj.noiDungSuaChua == "")
                    {
                        _update.noiDungSuaChua = null;

                    }
                    else
                    {
                        _update.noiDungSuaChua = _obj.noiDungSuaChua;
                    }
                    if (_obj.soLuong == null)
                    {
                        _update.soLuong = null;

                    }
                    else
                    {
                        _update.soLuong = _obj.soLuong;
                    }
                    if (_obj.donViTinh == null || _obj.donViTinh == "")
                    {
                        _update.donViTinh = null;

                    }
                    else
                    {
                        _update.donViTinh = _obj.donViTinh;
                    }
                    if (_obj.donGia == null)
                    {
                        _update.donGia = null;

                    }
                    else
                    {
                        _update.donGia = _obj.donGia;
                    }
                    _update.thanhTienChuaThue = _obj.thanhTienChuaThue;
                    _update.thueVat = _obj.thueVat;
                    _update.tongThanhToan = _obj.tongThanhToan;
                    if (_obj.no == null)
                    {
                        _update.no = 0;

                    }
                    else
                    {
                        _update.no = _obj.no;
                    }
                    if (_obj.canTru == null)
                    {
                        _update.canTru = 0;

                    }
                    else
                    {
                        _update.canTru = _obj.canTru;
                    }
                    if (_obj.ghiChu == null)
                    {
                        _update.ghiChu = null;

                    }
                    else
                    {
                        _update.ghiChu = _obj.ghiChu;
                    }
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public JsonResult GetDmXeVanTai()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            var data = _entities.vt_XeVanTai.Where(x => x.daXoa == "0").ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDmKhachHang()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            var data = _entities.vt_DMKhachHang.Where(x => x.daXoa == 0).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetXeSuaChuaById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _xeSuaChuaTaiXuongService.GetXeSuaChuaById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
            /*            return Json(_entities.vt_LichTrinhXe.FirstOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
            */
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idXeSuaChua = int.Parse(Request["idXeSuaChua"]);
                vt_XeSuaChuaTaiXuong _old = _entities.vt_XeSuaChuaTaiXuong.Find(idXeSuaChua);
                if (_old != null)
                {
                    _entities.vt_XeSuaChuaTaiXuong.Remove(_old);
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
                string idXeSuaChuaarr = Request["idXeSuaChua"];
                string[] idXeSuaChua = idXeSuaChuaarr.Split(' ');
                for (int i = 0; i < idXeSuaChua.Length; i++)
                {
                    vt_XeSuaChuaTaiXuong _old = _entities.vt_XeSuaChuaTaiXuong.Find(int.Parse(idXeSuaChua[i]));
                    if (_old != null)
                    {
                        _entities.vt_XeSuaChuaTaiXuong.Remove(_old);
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