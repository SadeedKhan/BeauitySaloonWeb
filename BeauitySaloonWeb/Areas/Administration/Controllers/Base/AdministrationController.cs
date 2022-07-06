using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers.Base
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
     
    }
}