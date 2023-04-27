using QLDV.Models;
using QLDV.Models.DAL.RoleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class HomeController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private RoleService _roleService = new RoleService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Sidebar(int nguoidungid)
        {
            ViewBag.Menu = _roleService.ListQuyenCha(nguoidungid);
            return PartialView("_SideBar");
        }

        [HttpPost]
        public ActionResult Login(string taikhoan, string password)
        {
            var loginInfo = _entities.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == taikhoan && x.MatKhau == password);
            if (loginInfo != null)
            {
                if (loginInfo.HasAccount == false)
                {
                    ViewBag.error = "<div class=\"alert alert-warning fade show\"><span class=\"close\" data-dismiss=\"alert\">&times;</span><strong>Người dùng chưa được cấp tài khoản!!</strong></div>";
                    return View();
                }
                else
                {
                    Session["NguoiDungId"] = loginInfo.NguoiDungId;
                    Session["TaiKhoan"] = loginInfo.TaiKhoan;
                    Session["TenDayDu"] = loginInfo.HoTen;
                    Session["QuyenKTDonVi"] = loginInfo.QuyenKhaiThacDonVi;
                    Session["QuyenKTToChuc"] = loginInfo.QuyenKhaiThacToChuc;
                    Session["DonViId"] = loginInfo.DonViId;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = "<div class=\"alert alert-warning fade show\"><span class=\"close\" data-dismiss=\"alert\">&times;</span><strong>Đăng nhập không thành công!!</strong></div>";
            return View();
        }

        public ActionResult Logout()
        {
            Session["NguoiDungId"] = 0;
            Session["TaiKhoan"] = null;
            Session["TenDayDu"] = null;
            Session["QuyenKTDonVi"] = 0;
            Session["QuyenKTToChuc"] = 0;
            Session["DonViId"] = 0;
            return View();
        }
    }
}