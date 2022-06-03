using BeauitySaloonWeb.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        // GET: Appointments
        public ActionResult Index()
        {
            return View();
        }
    }
}