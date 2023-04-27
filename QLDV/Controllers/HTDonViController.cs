using QLDV.Models;
using QLDV.Models.DAL.DonViService;
using QLDV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class HTDonViController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        public JsTreeAccess _jsUtil = new JsTreeAccess();
        public DonViService _dvsv = new DonViService();
        // GET: HTDonVi
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListDonVi()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            int iddonvikt = int.Parse(Request["iddonvikt"]);
            var exam = _entities.DonVis.Where(x => x.DonViId == iddonvikt || x.DonViCha == iddonvikt).ToList();
            return Json(new { data = exam }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDonViById()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            int iddonvi = int.Parse(Request.QueryString["DonViId"]);
            return Json(_entities.DonVis.FirstOrDefault(x => x.DonViId == iddonvi), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListTcByDonVi()
        { 
            int iddonvi = int.Parse(Request["iddonvi"]);
            return Json(new { data = _dvsv.GetListDvTc(iddonvi) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTcById()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            int idtochuc = int.Parse(Request["idtochuc"]);
            return Json(_entities.DonViToChuc_Map.FirstOrDefault(x => x.Id == idtochuc), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteToChucById()
        {
            int idtochuc = int.Parse(Request["idtochuc"]);
            try
            {
                var _obj = _entities.DonViToChuc_Map.Find(idtochuc);
                if (_obj != null)
                {
                    _entities.DonViToChuc_Map.Remove(_obj);
                    _entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }

        [HttpPost]
        public string UpdateDonVi(DonVi _objDonVi)
        {
            try
            {
                if (_objDonVi.DonViId == 0)
                {
                    DonVi _new = new DonVi();
                    _new.DonViCha = _objDonVi.DonViCha;
                    _new.DiaChi = _objDonVi.DiaChi;
                    _new.DienThoai = _objDonVi.DienThoai;
                    _new.Fax = _objDonVi.Fax;
                    _new.MoTa = _objDonVi.MoTa;
                    _new.TinhThanh = _objDonVi.TinhThanh;
                    _new.LoaiDonVi = _objDonVi.LoaiDonVi;
                    _new.TenDonVi = _objDonVi.TenDonVi;
                    _new.NgayTao = DateTime.Now;
                    _entities.DonVis.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    DonVi _update = _entities.DonVis.Find(_objDonVi.DonViId);
                    _update.DonViCha = _objDonVi.DonViCha;
                    _update.DiaChi = _objDonVi.DiaChi;
                    _update.DienThoai = _objDonVi.DienThoai;
                    _update.Fax = _objDonVi.Fax;
                    _update.MoTa = _objDonVi.MoTa;
                    _update.TinhThanh = _objDonVi.TinhThanh;
                    _update.LoaiDonVi = _objDonVi.LoaiDonVi;
                    _update.TenDonVi = _objDonVi.TenDonVi;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpGet]
        public string GetNameDonViById()
        {
            int iddonvi = int.Parse(Request.QueryString["DonViId"]);
            try
            {
                var _obj = _entities.DonVis.FirstOrDefault(x => x.DonViId == iddonvi);
                return _obj.TenDonVi;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
            
        }

        public JsonResult LoadTreeDonVi()
        {
            List<DonVi> lsDonVi = _entities.DonVis.ToList();
            int iddonvi = int.Parse(Session["QuyenKTDonVi"].ToString());
            var donvi = _entities.DonVis.Find(iddonvi);
            Node root = new Node();
            root.id = iddonvi;
            root.children = new List<Node>();
            root.text = donvi.TenDonVi;
            root.icon = "fa fa-folder-open fa-lg";
            List<DonVi> lsCha = new List<DonVi>();
            foreach (var item in lsDonVi)
            {
                if (item.DonViCha == iddonvi)
                {
                    lsCha.Add(item);
                }
            }
            foreach (var item in lsCha)
            {
                Node cNode = new Node();
                cNode.id = item.DonViId;
                cNode.text = item.TenDonVi;
                cNode.icon = "fa fa-users text-primary fa-lg";
                cNode.children = new List<Node>();
                root.children.Add(cNode);
                _jsUtil.AddChildItems(lsDonVi, cNode, item.DonViId);
            }
            return Json(root, JsonRequestBehavior.AllowGet);
        }

        //Load tree tổ chức theo đơn vị
        public JsonResult LoadTreeToChuc()
        {
            int iddonvi = int.Parse(Request["iddonvi_root"]);
            int idloaihtc = int.Parse(Request["idloaihtc"]);
            List<DonViToChuc_Map> lsToChuc = _entities.DonViToChuc_Map.Where(x => x.LoaiHinhToChucId == idloaihtc).ToList();
            var tochuc = _entities.DonViToChuc_Map.FirstOrDefault(x => x.DonViId == iddonvi && x.LoaiHinhToChucId == idloaihtc);
            Node root = new Node();
            try
            {
                root.id = tochuc.Id;
                root.children = new List<Node>();
                root.text = tochuc.TenToChuc;
                root.icon = "fa fa-folder-open fa-lg";
                List<DonViToChuc_Map> lsCha = new List<DonViToChuc_Map>();
                foreach (var item in lsToChuc)
                {
                    if (item.ToChucChaId == tochuc.Id)
                    {
                        lsCha.Add(item);
                    }
                }
                foreach (var item in lsCha)
                {
                    Node cNode = new Node();
                    cNode.id = item.Id;
                    cNode.text = item.TenToChuc;
                    cNode.icon = "fa fa-users text-primary fa-lg";
                    cNode.children = new List<Node>();
                    root.children.Add(cNode);
                    _jsUtil.AddChildItemsToChuc(lsToChuc, cNode, item.Id);
                }
                return Json(root, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(root, JsonRequestBehavior.AllowGet);
                throw;
            }
            
        }

        //load tree tổ chức theo quyền khai thác tổ chức
        public JsonResult LoadTreeToChucKT()
        {
            int idtochuc_root = int.Parse(Request["idtochuc_root"]);
            int idloaihtc = int.Parse(Request["idloaihtc"]);
            List<DonViToChuc_Map> lsToChuc = _entities.DonViToChuc_Map.Where(x => x.LoaiHinhToChucId == idloaihtc).ToList();
            var tochuc = _entities.DonViToChuc_Map.Find(idtochuc_root);
            Node root = new Node();
            try
            {
                root.id = tochuc.Id;
                root.children = new List<Node>();
                root.text = tochuc.TenToChuc;
                root.icon = "fa fa-folder-open fa-lg";
                List<DonViToChuc_Map> lsCha = new List<DonViToChuc_Map>();
                foreach (var item in lsToChuc)
                {
                    if (item.ToChucChaId == tochuc.Id)
                    {
                        lsCha.Add(item);
                    }
                }
                foreach (var item in lsCha)
                {
                    Node cNode = new Node();
                    cNode.id = item.Id;
                    cNode.text = item.TenToChuc;
                    cNode.icon = "fa fa-users text-primary fa-lg";
                    cNode.children = new List<Node>();
                    root.children.Add(cNode);
                    _jsUtil.AddChildItemsToChuc(lsToChuc, cNode, item.Id);
                }
                return Json(root, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(root, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}