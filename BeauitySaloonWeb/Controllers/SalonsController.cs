using AutoMapper;
using BeauitySaloonWeb.CustomsValidations;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Salons;
using BeauitySaloonWeb.Models.ViewModel.SalonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class SalonsController : Controller
    {
        // GET: Salons

        private static readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        public ActionResult Index( int? sortId, // categoryId
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            Mapper.CreateMap<Salon, SalonViewModel>();
            var viewModel = new SalonsPaginatedListViewModel();
            ViewData["CurrentSort"] = sortId;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = sortId;
            int pageSize = 8;
            var pageIndex = pageNumber ?? 1;
            var salons = Mapper.Map<IEnumerable<SalonViewModel>>(GetAllWithPaging(searchString, sortId));
            var salonsList = salons.ToList();
            var count = _applicationDbContext.Salons.Count();

            var list = new PaginatedList<SalonViewModel>(salonsList, count, pageIndex, pageSize);
            if (list.Any())
            {
                viewModel.Salons = list;
            }
            return View(viewModel);

        }

        public ActionResult Details(int id)
        {
            try
            {
                Mapper.CreateMap<Salon, SalonWithServicesViewModel>();
                SalonWithServicesViewModel viewModel = Mapper.Map<SalonWithServicesViewModel>(_applicationDbContext.Salons.Where(x => x.Id == id).SingleOrDefault());
                if(viewModel != null)
                {
                    Mapper.CreateMap<SalonService, SalonServiceViewModel>();
                    viewModel.Services = Mapper.Map<IEnumerable<SalonServiceViewModel>>(_applicationDbContext.SalonServices.Where(x => x.SalonId == viewModel.Id).ToList());
                    return this.View(viewModel);
                }
                else
                {
                    return View(new SalonWithServicesViewModel());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exceptions = ex.Message;
                return this.View("Error"); ;
            }
        }

        //Custom Methods
        private IEnumerable<Salon> GetAllWithPaging(string searchString, int? sortId)
        {
            IEnumerable<Salon> query = _applicationDbContext.Salons.OrderBy(x => x.Name);
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
            }
            if (sortId != null)
            {
                query = query.Where(x => x.CategoryId == sortId);
            }
            return query;
        }
    }
}