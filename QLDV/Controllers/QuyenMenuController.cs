using QLDV.Models;
using QLDV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class QuyenMenuController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        public JsTreeAccess _jsUtil = new JsTreeAccess();
        // GET: QuyenMenu
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListQuyen()
        {
            //_entities.Configuration.ProxyCreationEnabled = false;
            return Json(new { data = _entities.QuyenMenus.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string UpdateQuyen(QuyenMenu _objQuyen)
        {
            try
            {
                if (_objQuyen.QuyenMenuId == 0)
                {
                    QuyenMenu _new = new QuyenMenu();
                    _new.TenQuyen = _objQuyen.TenQuyen;
                    _new.QuyenCha = _objQuyen.QuyenCha;
                    _new.Styles = _objQuyen.Styles;
                    _new.Class = _objQuyen.Class;
                    _new.Controller = _objQuyen.Controller;
                    _new.Action = _objQuyen.Action;
                    _entities.QuyenMenus.Add(_new);
                    _entities.SaveChanges();
                    return "addsuccess";
                }
                else
                {
                    QuyenMenu _update = _entities.QuyenMenus.Find(_objQuyen.QuyenMenuId);
                    _update.TenQuyen = _objQuyen.TenQuyen;
                    _update.QuyenCha = _objQuyen.QuyenCha;
                    _update.Styles = _objQuyen.Styles;
                    _update.Class = _objQuyen.Class;
                    _update.Controller = _objQuyen.Controller;
                    _update.Action = _objQuyen.Action;
                    _entities.SaveChanges();
                    return "updatesuccess";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public JsonResult GetQuyenById()
        {
            int QuyenMenuId = int.Parse(Request.QueryString["QuyenMenuId"]);
            return Json(_entities.QuyenMenus.FirstOrDefault(x => x.QuyenMenuId == QuyenMenuId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetNameQuyenById()
        {
            int idquyenmenuid = int.Parse(Request.QueryString["QuyenMenuId"]);
            try
            {
                var _obj = _entities.QuyenMenus.FirstOrDefault(x => x.QuyenMenuId == idquyenmenuid);
                return _obj.TenQuyen;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }

        [HttpPost]
        public bool DeleteByID()
        {
            try
            {
                int idquyenmenu = int.Parse(Request["idquyenmenu"]);
                QuyenMenu _old = _entities.QuyenMenus.Find(idquyenmenu);
                if (_old != null)
                {
                    _entities.QuyenMenus.Remove(_old);
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
                string idquyenmenuarr = Request["idquyenmenu"];
                string[] idquyenmenu = idquyenmenuarr.Split(' ');
                for (int i = 0; i < idquyenmenu.Length; i++)
                {
                    QuyenMenu _old = _entities.QuyenMenus.Find(int.Parse(idquyenmenu[i]));
                    if (_old != null)
                    {
                        _entities.QuyenMenus.Remove(_old);
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

        public JsonResult LoadTreeQuyen()
        {
            List<QuyenMenu> lsQuyen = _entities.QuyenMenus.ToList();
            Node root = new Node();
            root.id = 0;
            root.children = new List<Node>();
            root.text = "Danh sách nhóm quyền";
            root.icon = "fa fa-folder-open fa-lg";
            List<QuyenMenu> lsCha = new List<QuyenMenu>();
            foreach (var item in lsQuyen)
            {
                if (item.QuyenCha == 0)
                {
                    lsCha.Add(item);
                }
            }
            foreach (var item in lsCha)
            {
                Node cNode = new Node();
                cNode.id = item.QuyenMenuId;
                cNode.text = item.TenQuyen;
                cNode.icon = "fa fa-users text-primary fa-lg";
                cNode.children = new List<Node>();
                root.children.Add(cNode);
                _jsUtil.AddChildItemsQuyen(lsQuyen, cNode, item.QuyenMenuId);
            }
            return Json(root, JsonRequestBehavior.AllowGet);
        }
    }
}