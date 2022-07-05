using AutoMapper;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminCitiesController : Controller
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            try
            {
                var viewModel = new CitiesListViewModel();
                IEnumerable<City> list = _applicationDbContext.Cities.ToList();
                Mapper.CreateMap<Category, CityViewModel>();
                if (list.Any())
                {
                    viewModel.Cities = Mapper.Map<IEnumerable<CityViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
                }
                return this.View(viewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult AddCity()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddCity(CityInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            _applicationDbContext.Cities.Add(new City
            {

                Name = input.Name,
            });
            _applicationDbContext.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCity(int id)
        {
            if (id <= 2)
            {
                return this.RedirectToAction("Index");
            }
            var obj = _applicationDbContext.Cities.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _applicationDbContext.Cities.Remove(obj);
                _applicationDbContext.SaveChanges();
            }
            return this.RedirectToAction("Index");
        }
    }
}