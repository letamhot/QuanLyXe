using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class ChucVuController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        // GET: ChucVu
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListChucVu()
        {
            return Json(new { data = _entities.ChucVus.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string UpdateChucVu(ChucVu _objChucVu)
        {
            try
            {
                if (_objChucVu.ChucVuId == 0)
                {
                    ChucVu _new = new ChucVu();
                    _new.TenChucVu = _objChucVu.TenChucVu;
                    _new.CapChucVu = _objChucVu.CapChucVu;
                    _new.LanhDao = _objChucVu.LanhDao;
                    _new.GuiEmail = _objChucVu.GuiEmail;
                    _new.GuiSMS = _objChucVu.GuiSMS;
                    _entities.ChucVus.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    ChucVu _update = _entities.ChucVus.Find(_objChucVu.ChucVuId);
                    _update.TenChucVu = _objChucVu.TenChucVu;
                    _update.CapChucVu = _objChucVu.CapChucVu;
                    _update.LanhDao = _objChucVu.LanhDao;
                    _update.GuiEmail = _objChucVu.GuiEmail;
                    _update.GuiSMS = _objChucVu.GuiSMS;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public JsonResult GetChucVuById()
        {
            int ChucVuId = int.Parse(Request.QueryString["ChucVuId"]);
            return Json(_entities.ChucVus.FirstOrDefault(x => x.ChucVuId == ChucVuId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idchucvu = int.Parse(Request["idchucvu"]);
                ChucVu _old = _entities.ChucVus.Find(idchucvu);
                if (_old != null)
                {
                    _entities.ChucVus.Remove(_old);
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
                string idchucvuarr = Request["idchucvu"];
                string[] idchucvu = idchucvuarr.Split(' ');
                for (int i = 0; i < idchucvu.Length; i++)
                {
                    ChucVu _old = _entities.ChucVus.Find(int.Parse(idchucvu[i]));
                    if (_old != null)
                    {
                        _entities.ChucVus.Remove(_old);
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