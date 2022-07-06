using AutoMapper;
using BeauitySaloonWeb.Areas.Manager.Controllers.Base;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Salons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Manager.Controllers
{
    public class ManagerSalonsController : BaseController
    {
        private static readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        public ActionResult Details(int id)
        {
            try
            {
                Mapper.CreateMap<Salon, SalonWithServicesViewModel>();
                var viewModel = new SalonWithServicesViewModel();
                viewModel = Mapper.Map<SalonWithServicesViewModel>(_applicationDbContext.SalonServices.Where(x => x.Id == id).SingleOrDefault());
                if (viewModel == null)
                {
                    return View(new SalonWithServicesViewModel());
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ChangeServiceAvailableStatus(string salonId, int serviceId)
        {
            try
            {
                var salonService = _applicationDbContext.SalonServices.Where(x => x.SalonId == salonId && x.ServiceId == serviceId).FirstOrDefault();
                salonService.Available = !salonService.Available;
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Details", new { id = salonId });
            }
            catch (Exception ex)
            {
              ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ConfirmAppointment(int id, string salonId)
        {
            try
            {
                var appointment = _applicationDbContext.Appointments.Where(x => x.Id == id).FirstOrDefault();
                appointment.Confirmed = true;
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Details", new { id = salonId });
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }

        [HttpPost]
        public  ActionResult DeclineAppointment(int id, string salonId)
        {
            var appointment = _applicationDbContext.Appointments.Where(x => x.Id == id).FirstOrDefault();
            appointment.Confirmed = false;
            _applicationDbContext.SaveChanges();
            return this.RedirectToAction("Details", new { id = salonId });
        }
    }
}