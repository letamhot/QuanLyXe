using QLDV.Models;
using QLDV.Models.DAL;
using QLDV.Models.DAL.NhomQuyenService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class NhomQuyenController : Controller
    {
        private SqlDataAccess _sqlAccess = new SqlDataAccess();
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private NhomQuyenService _nqservice = new NhomQuyenService();
        // GET: NhomQuyen
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNhomQuyen()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            return Json(new { data = _entities.NhomQuyens.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTreeQuyenMenu(int ID)
        {
            DataTable dt = new DataTable();
            JsTreeModel nodeRoot = new JsTreeModel { id = "0", parent = "#", text = "Danh sách quyền", icon = "fa fa-folder-open fa-lg", state = new States { opened = true } };
            var nodes = new List<JsTreeModel>
            {
                nodeRoot
            };
            dt = _nqservice.GetQuyenMain();
            DataTable dtSelected = new DataTable();
            dtSelected = _nqservice.GetRoleSelectedByNhomQuyenID(ID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                nodes.Add(new JsTreeModel() { id = dt.Rows[i]["QuyenMenuId"].ToString(), parent = dt.Rows[i]["QuyenCha"].ToString(), text = dt.Rows[i]["TenQuyen"].ToString(), state = new States { selected = false }, icon = "fa fa-users text-primary fa-lg" });
                DataTable dtChild = _nqservice.GetRoleChild(int.Parse(dt.Rows[i]["QuyenMenuId"].ToString()));
                if (dtChild.Rows.Count > 0)
                {
                    for (int k = 0; k < dtChild.Rows.Count; k++)
                    {
                        bool isChecked = false;
                        for (int j = 0; j < dtSelected.Rows.Count; j++)
                        {
                            if (dtSelected.Rows[j]["QuyenMenuId"].ToString() == dtChild.Rows[k]["QuyenMenuId"].ToString())
                            {
                                isChecked = true;
                                break;
                            }
                        }
                        nodes.Add(new JsTreeModel() { id = dtChild.Rows[k]["QuyenMenuId"].ToString(), parent = dt.Rows[i]["QuyenMenuId"].ToString(), text = dtChild.Rows[k]["TenQuyen"].ToString(), state = new States { selected = isChecked }, icon = "fa fa-user text-danger fa-lg" });
                    }
                }
            }
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        
        public bool SaveNhomQuyenMenu(int nhomquyenid, List<int> listquyenmenu)
        {
            bool deloldrole = _sqlAccess.DbCommand("delete from NhomQuyenMenu_Map where NhomQuyenId = " + nhomquyenid);
            if (deloldrole)
            {
                if (listquyenmenu.Count == 0)
                {
                    return true;
                }
                else
                {
                    listquyenmenu.Sort();
                    try
                    {
                        for (int i = 0; i < listquyenmenu.Count; i++)
                        {
                            if (listquyenmenu[i] != 0)
                            {
                                NhomQuyenMenu_Map _new = new NhomQuyenMenu_Map
                                {
                                    NhomQuyenId = nhomquyenid,
                                    QuyenMenuId = listquyenmenu[i]
                                };
                                _entities.NhomQuyenMenu_Map.Add(_new);
                                _entities.SaveChanges();
                                int parentid = _nqservice.GetParentIdByQuyenId(listquyenmenu[i]);
                                if (parentid != 0)
                                {
                                    if (!_nqservice.CheckExistParentNode(nhomquyenid, parentid))
                                    {
                                        NhomQuyenMenu_Map _roleParent = new NhomQuyenMenu_Map
                                        {
                                            NhomQuyenId = nhomquyenid,
                                            QuyenMenuId = parentid
                                        };
                                        _entities.NhomQuyenMenu_Map.Add(_roleParent);
                                        _entities.SaveChanges();
                                    }
                                }
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
            return false;
        }
    }
}