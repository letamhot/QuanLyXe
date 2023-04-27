using QLDV.Models;
using QLDV.Models.DAL.DichVuKinhDoanhKhac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class DichVuKinhDoanhKhacController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private DichvuKinhDoanhKhacService _dichvuKDKService = new DichvuKinhDoanhKhacService();

        // GET: DichVuKinhDoanhKhac
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListDichVuKDK()
        {
            return Json(new { data = _dichvuKDKService.GetListDichVuKDK() }, JsonRequestBehavior.AllowGet);
        }
        public string UpdateDichVuKinhDoanhKhac(vt_DichVuKinhDoanhKhac _obj)
        {
            try
            {
                if (_obj.id == 0)
                {
                    vt_DichVuKinhDoanhKhac _new = new vt_DichVuKinhDoanhKhac();
                    _new.loaiDichVuKhac = _obj.loaiDichVuKhac;
                    _new.ngayHoanThanh = _obj.ngayHoanThanh;
                    _new.tenCongTrinh = _obj.tenCongTrinh;
                    _new.doiTruong = _obj.doiTruong;
                    if (_obj.doanhThu == null)
                    {
                        _new.doanhThu = 0;
                    }
                    else
                    {
                        _new.doanhThu = _obj.doanhThu;

                    }
                    if (_obj.chiPhi == null)
                    {
                        _new.chiPhi = 0;
                    }
                    else
                    {
                        _new.chiPhi = _obj.chiPhi;

                    }
                    if (_obj.loiNhuan == null)
                    {
                        _new.loiNhuan = 0;
                    }
                    else
                    {
                        _new.loiNhuan = _obj.loiNhuan;

                    }
                    if (_obj.thanhToanLan1 == null)
                    {
                        _new.thanhToanLan1 = 0;
                    }
                    else
                    {
                        _new.thanhToanLan1 = _obj.thanhToanLan1;

                    }
                    if (_obj.thanhToanLan2 == null)
                    {
                        _new.thanhToanLan2 = 0;
                    }
                    else
                    {
                        _new.thanhToanLan2 = _obj.thanhToanLan2;

                    }
                    if (_obj.thanhToanLan3 == null)
                    {
                        _new.thanhToanLan3 = 0;
                    }
                    else
                    {
                        _new.thanhToanLan3 = _obj.thanhToanLan3;

                    }
                    if (_obj.no == null)
                    {
                        _new.no = 0;
                    }
                    else
                    {
                        _new.no = _obj.no;

                    }
                    _new.ghiChu = _obj.ghiChu;
                    _entities.vt_DichVuKinhDoanhKhac.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_DichVuKinhDoanhKhac _update = _entities.vt_DichVuKinhDoanhKhac.Find(_obj.id);
                    _update.loaiDichVuKhac = _obj.loaiDichVuKhac;
                    _update.ngayHoanThanh = _obj.ngayHoanThanh;
                    _update.tenCongTrinh = _obj.tenCongTrinh;
                    _update.doiTruong = _obj.doiTruong;
                    if (_obj.doanhThu == null)
                    {
                        _update.doanhThu = 0;
                    }
                    else
                    {
                        _update.doanhThu = _obj.doanhThu;

                    }
                    if (_obj.chiPhi == null)
                    {
                        _update.chiPhi = 0;
                    }
                    else
                    {
                        _update.chiPhi = _obj.chiPhi;

                    }
                    if (_obj.loiNhuan == null)
                    {
                        _update.loiNhuan = 0;
                    }
                    else
                    {
                        _update.loiNhuan = _obj.loiNhuan;

                    }
                    if (_obj.thanhToanLan1 == null)
                    {
                        _update.thanhToanLan1 = 0;
                    }
                    else
                    {
                        _update.thanhToanLan1 = _obj.thanhToanLan1;

                    }
                    if (_obj.thanhToanLan2 == null)
                    {
                        _update.thanhToanLan2 = 0;
                    }
                    else
                    {
                        _update.thanhToanLan2 = _obj.thanhToanLan2;

                    }
                    if (_obj.thanhToanLan3 == null)
                    {
                        _update.thanhToanLan3 = 0;
                    }
                    else
                    {
                        _update.thanhToanLan3 = _obj.thanhToanLan3;

                    }
                    if (_obj.no == null)
                    {
                        _update.no = 0;
                    }
                    else
                    {
                        _update.no = _obj.no;

                    }
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
        public JsonResult GetDichVuKinhDoanhKhacById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _dichvuKDKService.GetDichVuKhacById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int id = int.Parse(Request["id"]);
                vt_DichVuKinhDoanhKhac _old = _entities.vt_DichVuKinhDoanhKhac.Find(id);
                if (_old != null)
                {
                    _entities.vt_DichVuKinhDoanhKhac.Remove(_old);
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
                string idarr = Request["id"];
                string[] id = idarr.Split(' ');
                for (int i = 0; i < id.Length; i++)
                {
                    vt_DichVuKinhDoanhKhac _old = _entities.vt_DichVuKinhDoanhKhac.Find(int.Parse(id[i]));
                    if (_old != null)
                    {
                        _entities.vt_DichVuKinhDoanhKhac.Remove(_old);
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