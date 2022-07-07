using AutoMapper;
using BeauitySaloonWeb.Areas.Manager.Controllers.Base;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Salons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Manager.Controllers
{
    public class ManagerDashboardController : BaseController
    {
        private static readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        // GET: Manager/ManagerDashboard
        public ActionResult Index()
        {
            try
            {
                var viewModel = new SalonsSimpleListViewModel();
                IEnumerable<Salon> list = _applicationDbContext.Salons.ToList();
                Mapper.CreateMap<Salon, SalonSimpleViewModel>();
                if (list.Any() || list!=null)
                {
                    viewModel.Salons = Mapper.Map<IEnumerable<SalonSimpleViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
                }
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }
    }
}