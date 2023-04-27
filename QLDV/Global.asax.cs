using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QLDV
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            Session["NguoiDungId"] = 0;
            Session["TaiKhoan"] = null;
            Session["TenDayDu"] = null;
            Session["QuyenKTDonVi"] = 0;
            Session["QuyenKTToChuc"] = 0;
            Session["DonViId"] = 0;
        }
    }
}
