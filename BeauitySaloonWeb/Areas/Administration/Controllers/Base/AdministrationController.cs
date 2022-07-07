using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers.Base
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
     
    }
}