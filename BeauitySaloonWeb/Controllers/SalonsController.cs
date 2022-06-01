using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class SalonsController : Controller
    {
        // GET: Salons
        public ActionResult Index()
        {
            return View();
        }
    }
}