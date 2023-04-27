using QLDV.Models;
using QLDV.Models.DAL.XeVanTaiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class XeVanTaiController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private XeVanTaiService _xevantaiService = new XeVanTaiService();
        // GET: XeVanTai
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListXeVantai()
        {
            return Json(new { data = _xevantaiService.GetListXeVantai() }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetDmLaiXe()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            var data = _entities.vt_LaiXe.Where(x => x.daXoa == 0).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string UpdateXeVanTai(vt_XeVanTai _objXVT)
        {
            vt_LaiXe laiXe = _entities.vt_LaiXe.FirstOrDefault();
            try
            {
                if (_objXVT.id == 0)
                {
                    vt_XeVanTai _new = new vt_XeVanTai();
                    _new.maSoXe = _objXVT.maSoXe;
                    _new.idLaiXe = laiXe.id;
                    _new.daXoa = "0";
                    _entities.vt_XeVanTai.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    vt_XeVanTai _update = _entities.vt_XeVanTai.Find(_objXVT.id);
                    _update.maSoXe = _objXVT.maSoXe;
                    _update.idLaiXe = laiXe.id;
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
        public JsonResult GetXeVanTaiById()
        {
            int id = int.Parse(Request.QueryString["id"]);
            var data = _xevantaiService.GetXeVanTaiById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
            /*            return Json(_entities.vt_LichTrinhXe.FirstOrDefault(x => x.id == id), JsonRequestBehavior.AllowGet);
            */
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idXeVanTai = int.Parse(Request["idXeVanTai"]);
                vt_XeVanTai _old = _entities.vt_XeVanTai.Find(idXeVanTai);
                if (_old != null)
                {
                    _old.daXoa = "1";
                    _entities.vt_XeVanTai.Remove(_old);
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
                string idXeVanTaiarr = Request["idXeVanTai"];
                string[] idXeVanTai = idXeVanTaiarr.Split(' ');
                for (int i = 0; i < idXeVanTai.Length; i++)
                {
                    vt_XeVanTai _old = _entities.vt_XeVanTai.Find(int.Parse(idXeVanTai[i]));
                    if (_old != null)
                    {
                        _old.daXoa = "1";
                        _entities.vt_XeVanTai.Remove(_old);
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