using QLDV.Models;
using QLDV.Models.DAL;
using QLDV.Models.DAL.CapToChucService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class DmLoaiHTCController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private SqlDataAccess _sqlAccess = new SqlDataAccess();
        public CapToChucService _ctcSv = new CapToChucService();
        // GET: DmLoaiHTC
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLoaiHinhToChuc()
        {
            return Json(new { data = _entities.LoaiHinhToChucs.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoaiHinhToChucByDonVi()
        {
            List<LoaiHinhToChuc> rs = new List<LoaiHinhToChuc>();
            try
            {
                int iddonvi = int.Parse(Request["iddonvi"]);
                int idtochuc = int.Parse(Request["idtochuc"]);
                rs = _ctcSv.getListLoaiHTCByDonVi(iddonvi, idtochuc);
                return Json(rs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(rs, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public string UpdateLoaiHinhTC(LoaiHinhToChuc _objLoaiHTC)
        {
            try
            {
                if (_objLoaiHTC.LoaiHinhToChucId == 0)
                {
                    LoaiHinhToChuc _new = new LoaiHinhToChuc();
                    _new.TenLoaiHinhToChuc = _objLoaiHTC.TenLoaiHinhToChuc;
                    _new.MoTa = _objLoaiHTC.MoTa;
                    _entities.LoaiHinhToChucs.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    LoaiHinhToChuc _update = _entities.LoaiHinhToChucs.Find(_objLoaiHTC.LoaiHinhToChucId);
                    _update.TenLoaiHinhToChuc = _objLoaiHTC.TenLoaiHinhToChuc;
                    _update.MoTa = _objLoaiHTC.MoTa;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public JsonResult GetLoaiHTCById()
        {
            int LoaiHTCId = int.Parse(Request.QueryString["LoaiHTCId"]);
            _entities.Configuration.ProxyCreationEnabled = false;
            return Json(_entities.LoaiHinhToChucs.FirstOrDefault(x => x.LoaiHinhToChucId == LoaiHTCId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idloaihtc = int.Parse(Request["idloaihtc"]);
                LoaiHinhToChuc _old = _entities.LoaiHinhToChucs.Find(idloaihtc);
                if (_old != null)
                {
                    _entities.LoaiHinhToChucs.Remove(_old);
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
                string idloaihtcarr = Request["idloaihtc"];
                string[] idloaihtc = idloaihtcarr.Split(' ');
                for (int i = 0; i < idloaihtc.Length; i++)
                {
                    LoaiHinhToChuc _old = _entities.LoaiHinhToChucs.Find(int.Parse(idloaihtc[i]));
                    if (_old != null)
                    {
                        _entities.LoaiHinhToChucs.Remove(_old);
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

        public JsonResult GetCapToChucByLoaiHTC()
        {
            List<CapToChuc> rs = new List<CapToChuc>();
            try
            {
                int idloaihtc = int.Parse(Request["idloaihtc"]);
                int iddonvi = int.Parse(Request["iddonvi"]);
                int idtochuc = int.Parse(Request["idtochuc"]);
                rs = _ctcSv.getCapToChucByLoaiHTCExceptDonVi(idloaihtc, iddonvi, idtochuc);
                return Json(rs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(rs, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult GetCapToChucByLoaiHTCId()
        {
            List<CapToChuc> rs = new List<CapToChuc>();
            try
            {
                int idloaihtc = int.Parse(Request["idloaihtc"]);
                return Json(_ctcSv.getCapToChucByLoaiHTCId(idloaihtc), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(rs, JsonRequestBehavior.AllowGet);
                throw;
            }
            
        }

        [HttpPost]
        public string UpdateCapToChucDonVi(DonViToChuc_Map _obj)
        {
            try
            {
                if (_obj.Id == 0)
                {
                    DonViToChuc_Map _new = new DonViToChuc_Map();
                    _new.LoaiHinhToChucId = _obj.LoaiHinhToChucId;
                    _new.CapToChucId = _obj.CapToChucId;
                    _new.NgayThanhLap = _obj.NgayThanhLap;
                    _new.DonViId = _obj.DonViId;
                    var _dv = _entities.DonVis.FirstOrDefault(x => x.DonViId == _obj.DonViId);
                    var _captc = _entities.CapToChucs.FirstOrDefault(x => x.CapToChucId == _obj.CapToChucId);
                    if (_dv.DonViCha == 0)
                    {
                        _new.ToChucChaId = 0;
                    }
                    else
                    {
                        var _dvtc = _entities.DonViToChuc_Map.FirstOrDefault(x => x.DonViId == _dv.DonViCha && x.LoaiHinhToChucId == _obj.LoaiHinhToChucId);
                        if (_dvtc != null)
                        {
                            _new.ToChucChaId = _dvtc.Id;
                        }
                    }
                    _new.TenToChuc = _captc.TenCapToChuc + " " + _dv.TenDonVi;
                    _entities.DonViToChuc_Map.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    DonViToChuc_Map _update = _entities.DonViToChuc_Map.Find(_obj.Id);
                    _update.LoaiHinhToChucId = _obj.LoaiHinhToChucId;
                    _update.CapToChucId = _obj.CapToChucId;
                    _update.NgayThanhLap = _obj.NgayThanhLap;
                    _update.DonViId = _obj.DonViId;
                    var _dv = _entities.DonVis.FirstOrDefault(x => x.DonViId == _obj.DonViId);
                    var _captc = _entities.CapToChucs.FirstOrDefault(x => x.CapToChucId == _obj.CapToChucId);
                    if (_dv.DonViCha == 0)
                    {
                        _update.ToChucChaId = 0;
                    }
                    else
                    {
                        var _dvtc = _entities.DonViToChuc_Map.FirstOrDefault(x => x.DonViId == _dv.DonViCha && x.LoaiHinhToChucId == _obj.LoaiHinhToChucId);
                        if (_dvtc != null)
                        {
                            _update.ToChucChaId = _dvtc.Id;
                        }
                    }
                    _update.TenToChuc = _captc.TenCapToChuc + " " + _dv.TenDonVi;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public bool CheckLoaiHTC()
        {
            int idtochuc = int.Parse(Request["idtochuc"]);
            int iddonvi = int.Parse(Request["iddonvi"]);
            int idloaihtc = int.Parse(Request["idloaihtc"]);
            string sql = "select * from DonViToChuc_Map where LoaiHinhToChucId = " + idloaihtc + " and DonViId = " + iddonvi + " and Id != " + idtochuc;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public string GetLoaiHTCIdByIdToChuc()
        {
            try
            {
                int idtochuc = int.Parse(Request["idtochuc"]);
                var _obj = _entities.DonViToChuc_Map.Find(idtochuc);
                return _obj.LoaiHinhToChucId.ToString();
            }
            catch (Exception)
            {
                return "";
                throw;
            } 
        }

        [HttpGet]
        public string GetToChucIDByDonVi_LoaiHTC()
        {
            try
            {
                int iddonvi = int.Parse(Request["iddonvi"]);
                int idloaihtc = int.Parse(Request["idloaihtc"]);
                return _entities.DonViToChuc_Map.FirstOrDefault(x => x.DonViId == iddonvi && x.LoaiHinhToChucId == idloaihtc).Id.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
            
        }
    }
}