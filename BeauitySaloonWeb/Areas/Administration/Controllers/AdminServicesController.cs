using AutoMapper;
using BeauitySaloonWeb.Areas.Administration.Controllers.Base;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminServicesController : AdministrationController
    {
        private static readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        // GET: Administration/Services
        public ActionResult Index()
        {
            var viewModel = new ServicesListViewModel();
            IEnumerable<Service> list = _applicationDbContext.Services.ToList();
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
            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

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
            var serviceId = _applicationDbContext.Services.Add(new Service
            {
                Name = input.Name,
                CategoryId = input.CategoryId,
                Description = input.Description,
                CreatedOn=DateTime.Now
            });
            _applicationDbContext.SaveChanges();

            // Add to the Salon all Services from its Category
            var salonsIds = _applicationDbContext.Salons.Where(x => x.CategoryId == input.CategoryId).OrderBy(x => x.Id).Select(x => x.Id).ToList();

            foreach (var salonId in salonsIds)
            {
                _applicationDbContext.SalonServices.Add(new SalonService
                {
                    SalonId = salonId.ToString(),
                    ServiceId = serviceId.Id,
                    Available = true,
                    CreatedOn = DateTime.Now
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