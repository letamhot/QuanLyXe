using QLDV.Models;
using QLDV.Models.DAL.ProcedureService;
using QLDV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class ProcedureController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private ProcedureService _prosv = new ProcedureService();
        // GET: Procedure
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListQuyTrinhByBienDong()
        {
            List<ProcedureCustomModel> rs = new List<ProcedureCustomModel>();
            try
            {
                int idbiendong = int.Parse(Request["idbiendong"]);
                return Json(new { data = _prosv.getListQuyTrinhByBienDongId(idbiendong) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(rs, JsonRequestBehavior.AllowGet);
                throw;
            }
            
        }

        public JsonResult GetListQuyTrinhChiTiet()
        {
            int idquytrinh = int.Parse(Request["QuyTrinhId"]);
            return Json(new { data = _prosv.getListQuyTrinhChiTietById(idquytrinh) }, JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult LoadTreeBienDong()
        {
            Node root = new Node();
            root.id = 0;
            root.children = new List<Node>();
            root.text = "Danh sách biến động";
            root.icon = "fa fa-folder-open fa-lg";
            var _biendong = _entities.BienDongs.ToList();
            foreach (var item in _biendong)
            {
                Node cNode = new Node();
                cNode.id = item.BienDongId;
                cNode.text = item.TenBienDong;
                cNode.icon = "fa fa-arrow-circle-right text-primary fa-lg";
                cNode.children = new List<Node>();
                root.children.Add(cNode);
            }
            return Json(root, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string UpdateProcedure(BienDong_QuyTrinh _obj)
        {
            try
            {
                if (_obj.QuyTrinhId == 0)
                {
                    BienDong_QuyTrinh _new = new BienDong_QuyTrinh();
                    _new.TenQuyTrinh = _obj.TenQuyTrinh;
                    _new.LoaiHinhToChucId = _obj.LoaiHinhToChucId;
                    _new.CapToChucId = _obj.CapToChucId;
                    _new.GhiChu = _obj.GhiChu;
                    _new.NgayTao = DateTime.Now;
                    _new.BienDongId = _obj.BienDongId;
                    _entities.BienDong_QuyTrinh.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    BienDong_QuyTrinh _update = _entities.BienDong_QuyTrinh.Find(_obj.QuyTrinhId);
                    _update.TenQuyTrinh = _obj.TenQuyTrinh;
                    _update.LoaiHinhToChucId = _obj.LoaiHinhToChucId;
                    _update.CapToChucId = _obj.CapToChucId;
                    _update.GhiChu = _obj.GhiChu;
                    _update.BienDongId = _obj.BienDongId;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
            
        }

        [HttpPost]
        public string UpdateQuyTrinhChiTiet()
        {
            try
            {
                int idquytrinh = int.Parse(Request["QuyTrinhId"]);
                int loaihtcid = int.Parse(Request["LoaiHinhToChucId"]);
                int captcid = int.Parse(Request["CapToChucId"]);
                string email = Request["email"];
                string sms = Request["sms"];

                BienDong_QuyTrinh_ChiTiet _new = new BienDong_QuyTrinh_ChiTiet
                {
                    QuyTrinhId = idquytrinh,
                    LoaiHinhToChucId = loaihtcid,
                    CapToChucId = captcid,
                    Email = email,
                    Sms = sms
                };
                _entities.BienDong_QuyTrinh_ChiTiet.Add(_new);
                _entities.SaveChanges();
                return "addsuccess";
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        public bool DeleteQuyTrinhChiTietById()
        {
            try
            {
                int id = int.Parse(Request["QuyTrinhChiTietId"]);
                var _delete = _entities.BienDong_QuyTrinh_ChiTiet.Find(id);
                if (_delete != null)
                {
                    _entities.BienDong_QuyTrinh_ChiTiet.Remove(_delete);
                    _entities.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}