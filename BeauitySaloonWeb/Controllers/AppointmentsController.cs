using AutoMapper;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Appointments;
using BeauitySaloonWeb.Models.ViewModel.SalonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class AppointmentsController : Controller
    {       

        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakeAnAppointment(string salonId, int serviceId)
        {
            try
            {
                Mapper.CreateMap<Salon, SalonServiceSimpleViewModel>();
                var salonService = Mapper.Map<SalonServiceSimpleViewModel>(_applicationDbContext.SalonServices.Where(x=>x.SalonId==salonId &&
                    x.ServiceId == serviceId).SingleOrDefault());

                if (salonService == null || !salonService.Available)
                {
                    ViewBag.Exceptions = "No Appointment Service Found...!";
                    return this.View("Error");
                }

                var viewModel = new AppointmentInputModel
                {
                    SalonId = salonId,
                    ServiceId = serviceId,
                };
                return this.View(viewModel);
            }
            catch(Exception ex)
            {
                ViewBag.Exceptions = ex.Message.ToString();
                return this.View("Error");
            }
        } 

        public ActionResult RatePastAppointment(string id)
        {
            try
            {
                var viewModel = new SalonServiceSimpleViewModel();
                Mapper.CreateMap<Salon, AppointmentRatingViewModel>();
                SalonService obj = _applicationDbContext.SalonServices.Where(x => x.SalonId == id).SingleOrDefault();
                if (obj != null)
                {
                    viewModel = Mapper.Map<SalonServiceSimpleViewModel>(obj);
                }
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Exceptions = ex.Message;
                return this.View("Error");
            }
           
        } 

        [HttpGet]
        public ActionResult CancelAppointment(string id)
        {
            try
            {
                var viewModel = new SalonServiceSimpleViewModel();
                Mapper.CreateMap<Salon, AppointmentsController>();
                SalonService obj = _applicationDbContext.SalonServices.Where(x => x.SalonId == id).SingleOrDefault();


                if (obj != null)
                {
                    viewModel = Mapper.Map<SalonServiceSimpleViewModel>(obj);
                }

                return this.View(viewModel);

            }
            catch (Exception ex)
            {
                ViewBag.Exceptions = ex.Message;
                return this.View("Error");
            }
            
        } 

    }
}