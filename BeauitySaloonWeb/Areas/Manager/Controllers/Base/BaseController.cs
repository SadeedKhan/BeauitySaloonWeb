using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Manager.Controllers.Base
{
    [Authorize(Roles = "Manager")]
    public class BaseController : Controller
    {
        
    }
}