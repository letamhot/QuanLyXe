using QLDV.Models;
using QLDV.Models.DAL.ChiPhiLichTrinhXe;
using QLDV.Models.DAL.LichTrinhService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class ChiPhiLichTrinhXeController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private LichTrinhService _lichtrinhService = new LichTrinhService();
        private ChiPhiLTXService _chiphilichtrinhService = new ChiPhiLTXService();
        // GET: ChiPhiLichTrinhXe
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListChiPhiLichTrinhXe()
        {
            return Json(new { data = _chiphilichtrinhService.getDanhSachChiPhiLichTrinh() }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetLichTrinhXe()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            var data = _entities.vt_LichTrinhXe.Where(x => x.daXoa == 0).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string UpdateChiPhiLichTrinhXe(vt_ChiPhiLichTrinhXe _objCPLTX)
        {
            vt_LichTrinhXe lichtrinh = _entities.vt_LichTrinhXe.FirstOrDefault();
            try
            {
                if (_objCPLTX.id == 0)
                {
                    vt_ChiPhiLichTrinhXe _new = new vt_ChiPhiLichTrinhXe();
                    _new.idLichTrinhXe = _objCPLTX.idLichTrinhXe;
                    _new.loaiChiPhi = _objCPLTX.loaiChiPhi;
                    _new.tenChiPhi = _objCPLTX.tenChiPhi;
                    _new.giaTien = _objCPLTX.giaTien;
                    _new.daXoa = "0";
                    _entities.vt_ChiPhiLichTrinhXe.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_ChiPhiLichTrinhXe _update = _entities.vt_ChiPhiLichTrinhXe.Find(_objCPLTX.id);
                    _update.idLichTrinhXe = _objCPLTX.idLichTrinhXe;
                    _update.loaiChiPhi = _objCPLTX.loaiChiPhi;
                    _update.tenChiPhi = _objCPLTX.tenChiPhi;
                    _update.giaTien = _objCPLTX.giaTien;
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
        public JsonResult GetChiPhiLichTrinhXeById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _entities.vt_ChiPhiLichTrinhXe.Find(id);
            return Json(data, JsonRequestBehavior.AllowGet);
            /*            return Json(_entities.vt_LichTrinhXe.FirstOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
            */
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idchiphilichtrinhxe = int.Parse(Request["idchiphilichtrinhxe"]);
                vt_ChiPhiLichTrinhXe _old = _entities.vt_ChiPhiLichTrinhXe.Find(idchiphilichtrinhxe);
                if (_old != null)
                {
                    _old.daXoa = "1";
                    _entities.vt_ChiPhiLichTrinhXe.Remove(_old);
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
                string idchiphilichtrinhxearr = Request["idchiphilichtrinhxe"];
                string[] idchiphilichtrinhxe = idchiphilichtrinhxearr.Split(' ');
                for (int i = 0; i < idchiphilichtrinhxe.Length; i++)
                {
                    vt_ChiPhiLichTrinhXe _old = _entities.vt_ChiPhiLichTrinhXe.Find(int.Parse(idchiphilichtrinhxe[i]));
                    if (_old != null)
                    {
                        _old.daXoa = "1";
                        _entities.vt_ChiPhiLichTrinhXe.Remove(_old);
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