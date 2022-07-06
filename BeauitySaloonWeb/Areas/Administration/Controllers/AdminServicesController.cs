using AutoMapper;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminServicesController : Controller
    {
        private static readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        // GET: Administration/Services
        public ActionResult Index()
        {
            var viewModel = new ServicesListViewModel();
            IEnumerable<City> list = _applicationDbContext.Cities.ToList();
            Mapper.CreateMap<Service, ServiceViewModel>();
            if (list.Any())
            {
                viewModel.Services = Mapper.Map<IEnumerable<ServiceViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
            }
            return this.View(viewModel);
        }

        public ActionResult AddService()
        {
            var categories = _applicationDbContext.Categories.ToList();
            this.ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public ActionResult AddService(ServiceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // Add Service
           _applicationDbContext.Services.Add(new Service
            {
                Name = input.Name,
                CategoryId = input.CategoryId,
                Description = input.Description,
            });
            var serviceId = _applicationDbContext.SaveChanges();

            // Add to the Salon all Services from its Category
            var salonsIds = _applicationDbContext.Salons.Where(x => x.CategoryId == input.CategoryId).OrderBy(x => x.Id).Select(x => x.Id).ToList();

            foreach (var salonId in salonsIds)
            {
                _applicationDbContext.SalonServices.Add(new SalonService
                {
                    SalonId = salonId.ToString(),
                    ServiceId = serviceId,
                    Available = true,
                });
                _applicationDbContext.SaveChanges();
            }
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteService(int id)
        {
            if (id <= 55)
            {
                return this.RedirectToAction("Index");
            }

            var obj = _applicationDbContext.Services.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _applicationDbContext.Services.Remove(obj);
                _applicationDbContext.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }
    }
}