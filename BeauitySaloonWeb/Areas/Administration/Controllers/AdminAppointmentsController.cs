using AutoMapper;
using BeauitySaloonWeb.Areas.Administration.Controllers.Base;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminAppointmentsController : AdministrationController
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        // GET: Administration/Appointments
        public ActionResult Index()
        {
            Mapper.CreateMap<Appointment, AppointmentViewModel>();
            var viewModel = new AppointmentsListViewModel();
            var list = _applicationDbContext.Appointments.AsEnumerable().OrderByDescending(x => x.DateTime).ToList();
            if (list != null && list.Any())
            {
                viewModel.Appointments = Mapper.Map<IEnumerable<AppointmentViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
            }
            return View(viewModel);
        }
    }
}