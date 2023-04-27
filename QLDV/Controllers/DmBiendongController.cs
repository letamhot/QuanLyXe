using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class DmBiendongController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        // GET: DmBiendong
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBienDong()
        {
            //_entities.Configuration.ProxyCreationEnabled = false;
            return Json(new { data = _entities.BienDongs.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListBienDongById()
        {
            int idloaihtc = int.Parse(Request["idloaihtc"]);
            return Json(new { data = _entities.BienDongs.Where(x => x.LoaiHinhToChucId == idloaihtc && x.ShowList == true).ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string UpdateBienDong(BienDong _objBienDong)
        {
            try
            {
                if (_objBienDong.BienDongId == 0)
                {
                    BienDong _new = new BienDong();
                    _new.TenBienDong = _objBienDong.TenBienDong;
                    _new.MoTa = _objBienDong.MoTa;
                    _entities.BienDongs.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    BienDong _update = _entities.BienDongs.Find(_objBienDong.BienDongId);
                    _update.TenBienDong = _objBienDong.TenBienDong;
                    _update.MoTa = _objBienDong.MoTa;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        } 

        public JsonResult GetBienDongById()
        {
            int BienDongId = int.Parse(Request.QueryString["BienDongId"]);
            return Json(_entities.BienDongs.FirstOrDefault(x => x.BienDongId == BienDongId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idbiendong = int.Parse(Request["idbiendong"]);
                BienDong _old = _entities.BienDongs.Find(idbiendong);
                if (_old != null)
                {
                    _entities.BienDongs.Remove(_old);
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
                string idbiendongarr = Request["idbiendong"];
                string[] idbiendong = idbiendongarr.Split(' ');
                for (int i = 0; i < idbiendong.Length; i++)
                {
                    BienDong _old = _entities.BienDongs.Find(int.Parse(idbiendong[i]));
                    if (_old != null)
                    {
                        _entities.BienDongs.Remove(_old);
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