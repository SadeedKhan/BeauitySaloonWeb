using BeauitySaloonWeb.Areas.Administration.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminDashboardController : AdministrationController
    {
        // GET: Administration/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}