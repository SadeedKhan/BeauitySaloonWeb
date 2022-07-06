using BeauitySaloonWeb.Areas.Manager.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Manager.Controllers
{
    public class ManagerDashboardController : BaseController
    {
        // GET: Manager/ManagerDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}