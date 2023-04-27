using QLDV.Models;
using QLDV.Models.DAL;
using QLDV.Models.DAL.CapToChucService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class CapToChucController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        CapToChucService _ctcservice = new CapToChucService();
        // GET: CapToChuc
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListCapToChuc(int id)
        {
            return Json(new { data = _ctcservice.getListCapToChucSelected(id) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListCapToChucByLoaiHTC(int id)
        {
            return Json(new { data = _ctcservice.getListCapToChucByLoaiHtc(id) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool CapToChucForNhomQuyen()
        {
            var nhomquyenid = Request["nhomquyenid"];
            var idtc = Request["idtochuc"];
            string[] ids = idtc.Split(' ');
            bool delOld = _sqlAccess.DbCommand("delete from NhomQuyenCapTc_Map where NhomQuyenId = " + nhomquyenid);
            if (delOld)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    NhomQuyenCapTc_Map _obj = new NhomQuyenCapTc_Map();
                    _obj.NhomQuyenId = int.Parse(nhomquyenid);
                    _obj.CapToChucId = int.Parse(ids[i]);
                    _entities.NhomQuyenCapTc_Map.Add(_obj);
                }
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool CapToChucForLoaiHTC()
        {
            var idloaihtc = Request["idloaihtc"];
            var idtc = Request["idtochuc"];
            string[] ids = idtc.Split(' ');
            bool delOld = _sqlAccess.DbCommand("delete from CapToChucLoaiHinh_Map where LoaiHinhToChucId = " + idloaihtc);
            if (delOld)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    CapToChucLoaiHinh_Map _obj = new CapToChucLoaiHinh_Map();
                    _obj.LoaiHinhToChucId = int.Parse(idloaihtc);
                    _obj.CapToChucId = int.Parse(ids[i]);
                    _entities.CapToChucLoaiHinh_Map.Add(_obj);
                }
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpGet]
        public string GetTenCapTcById()
        {
            int idtochuc = int.Parse(Request["idtochuc"]);
            var _obj = _entities.CapToChucs.FirstOrDefault(x => x.CapToChucId == idtochuc);
            return _obj.TenCapToChuc;
        }

        [HttpGet]
        public int GetCapToChucIdByToChucId()
        {
            try
            {
                int idtochuc = int.Parse(Request["idtochuc"]);
                return _entities.DonViToChuc_Map.Find(idtochuc).CapToChucId;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
            
        }

        [HttpGet]
        public string GetTenToChucById()
        {
            int idtochuc = int.Parse(Request["idtochuc"]);
            return _entities.DonViToChuc_Map.Find(idtochuc).TenToChuc;
        }
    }
}