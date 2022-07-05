using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers.Base
{
    public class BaseController : Controller
    {
        // GET: Administration/Base
        public ActionResult Index()
        {
            return View();
        }
    }
}