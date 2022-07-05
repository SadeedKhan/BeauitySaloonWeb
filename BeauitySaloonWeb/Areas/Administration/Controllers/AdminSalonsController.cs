using AutoMapper;
using BeauitySaloonWeb.CustomsValidations;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Salons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminSalonsController : Controller
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        // GET: Administration/Salons
        public ActionResult Index()
        {
            var viewModel = new SalonsListViewModel();
            IEnumerable<City> list = _applicationDbContext.Cities.ToList();
            Mapper.CreateMap<Salon,SalonViewModel>();
            if (list.Any())
            {
                viewModel.Salons = Mapper.Map<IEnumerable<SalonViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
            }
            return this.View(viewModel);
        }

        public ActionResult AddSalon()
        {
            var categories = _applicationDbContext.Categories.ToList();
            var cities = _applicationDbContext.Cities.ToList();

            this.ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            this.ViewData["Cities"] = new SelectList(cities, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public ActionResult AddSalon(SalonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string imageUrl;
            try
            {
                imageUrl = UploadPicture.WriteFile(input.Image, "Salons");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }

            // Add Salon
            _applicationDbContext.Salons.Add(new Salon
            {
                Name = input.Name,
                CategoryId = input.CategoryId,
                CityId = input.CityId,
                Address = input.Address,
                ImageUrl = imageUrl,
                Rating = 0,
                RatersCount = 0,
            });
            var salonId = _applicationDbContext.SaveChanges();

            // Add to the Salon all Services from its Category
            var servicesIds = _applicationDbContext.Services.Where(x => x.CategoryId == input.CategoryId).OrderBy(x => x.Id).Select(x => x.Id).ToList();

            foreach (var serviceId in servicesIds)
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
        public ActionResult DeleteSalon(string id)
        {
            if (id.StartsWith("seeded"))
            {
                return this.RedirectToAction("Index");
            }

            var obj = _applicationDbContext.SalonServices.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefault();
            if (obj != null)
            {
                _applicationDbContext.SalonServices.Remove(obj);
                _applicationDbContext.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }
    }
}