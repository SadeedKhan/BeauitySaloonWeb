using AutoMapper;
using BeauitySaloonWeb.Customs;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Appointments;
using BeauitySaloonWeb.Models.ViewModel.SalonServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {

        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        private static UserManager<ApplicationUser> _UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_applicationDbContext));

        // GET: Appointments
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();
            Mapper.CreateMap<Appointment, AppointmentViewModel>();
            var viewModel = new AppointmentsListViewModel();
            var list = (from a in _applicationDbContext.Appointments
                        join s in _applicationDbContext.Salons on a.SalonId equals s.Id.ToString()
                        join se in _applicationDbContext.Services on a.ServiceId equals se.Id
                        join c in _applicationDbContext.Cities on s.CityId equals c.Id
                        where a.UserId == UserId && a.DateTime > DateTime.Now
                        orderby a.DateTime
                        select new AppointmentViewModel
                        {
                            Id = a.Id.ToString(),
                            Confirmed = a.Confirmed,
                            DateTime = a.DateTime,
                            IsSalonRatedByTheUser = a.IsSalonRatedByTheUser,
                            SalonAddress = s.Address,
                            SalonCityName = c.Name,
                            SalonId = a.SalonId,
                            SalonName = s.Name,
                            ServiceId = a.ServiceId,
                            ServiceName = se.Name
                        }
                       ).ToList();

            if (list != null && list.Any())
            {
                viewModel.Appointments = Mapper.Map<IEnumerable<AppointmentViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
            }
            return View(viewModel);
        }

        public ActionResult MakeAnAppointment(string salonId, string serviceId)
        {
            try
            {
                Mapper.CreateMap<SalonService, SalonServiceSimpleViewModel>();
                int ServiceId = Convert.ToInt32(serviceId);
                var salonService = Mapper.Map<SalonServiceSimpleViewModel>(_applicationDbContext.SalonServices.Where(x => x.SalonId == salonId &&
                    x.ServiceId == ServiceId).SingleOrDefault());
                if (salonService == null || !salonService.Available)
                {
                    ViewBag.Exceptions = "No Appointment Service Found...!";
                    return this.View("Error");
                }

                var viewModel = new AppointmentInputModel
                {
                    SalonId = salonId,
                    ServiceId = ServiceId,
                };
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Exceptions = ex.Message.ToString();
                return this.View("Error");
            }
        }

        [HttpPost]
        public ActionResult MakeAnAppointment(AppointmentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("MakeAnAppointment", new { input.SalonId, input.ServiceId });
            }
            try
            {
                DateTime dateTime;
                dateTime = DateTimeFormats.ConvertStrings(input.Date, input.Time);
                var userId = User.Identity.GetUserId();
                _applicationDbContext.Appointments.Add(new Appointment
                {
                    DateTime = dateTime,
                    UserId = userId,
                    SalonId = input.SalonId,
                    ServiceId = input.ServiceId,
                    CreatedOn = DateTime.Now,
                });
                _applicationDbContext.SaveChanges();
                return this.RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Exceptions = ex.Message;
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
        public ActionResult CancelAppointment(int id)
        {
            try
            {


                //var viewModel = appointmentsService.GetByIdAsync<AppointmentViewModel>(id);

                var viewModel = (from a in _applicationDbContext.Appointments
                            join s in _applicationDbContext.Salons on a.SalonId equals s.Id.ToString()
                            join se in _applicationDbContext.Services on a.ServiceId equals se.Id
                                 join c in _applicationDbContext.Cities on s.CityId equals c.Id
                                 where a.Id == id
                                 select new AppointmentViewModel
                            {
                                Id = a.Id.ToString(),
                                IsSalonRatedByTheUser = a.IsSalonRatedByTheUser,
                                SalonAddress = s.Address,
                                SalonId = a.SalonId,
                                SalonName = s.Name,
                                ServiceId = a.ServiceId,
                                ServiceName = se.Name,
                                SalonCityName=c.Name
                                
                            }
                      ).FirstOrDefault();
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Exceptions = ex.Message;
                return this.View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteAppointment(int id)
        {
            try
            {
                var obj = _applicationDbContext.Appointments.Where(x => x.Id == id).FirstOrDefault();
                if (obj != null)
                {
                    _applicationDbContext.Appointments.Remove(obj);
                    _applicationDbContext.SaveChanges();
                }
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }

        public ActionResult GetSalonServiceDetailsForAppointment(int salonId, int serviceId)
        {
            try
            {
                var viewModel = new SalonServiceDetailsViewModel();
                var salon = _applicationDbContext.Salons.Where(x => x.Id == salonId).SingleOrDefault();
                if (salon != null)
                {
                    viewModel.SalonId = salon.Id.ToString();
                    viewModel.SalonName = salon.Name;
                    viewModel.SalonAddress = salon.Address;
                    var city = _applicationDbContext.Cities.Where(x => x.Id == salon.CityId).SingleOrDefault();
                    if (city != null)
                    {
                        viewModel.SalonCityName = city.Name;
                        var service = _applicationDbContext.Services.Where(x => x.Id == serviceId).SingleOrDefault();
                        if (service != null)
                        {
                            viewModel.ServiceId = service.Id;
                            viewModel.ServiceName = service.Name;
                        }
                    }
                }
                return PartialView("_SalonServiceDetailsForAppointment", viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }
    }
}