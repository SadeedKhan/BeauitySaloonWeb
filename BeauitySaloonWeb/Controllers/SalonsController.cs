﻿using AutoMapper;
using BeauitySaloonWeb.CustomsValidations;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Categories;
using BeauitySaloonWeb.Models.ViewModel.Salons;
using BeauitySaloonWeb.Models.ViewModel.SalonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class SalonsController : Controller
    {
        // GET: Salons

        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View();  
        }

        public ActionResult Index( int? sortId, // categoryId
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            Mapper.CreateMap<Category, CategorySimpleViewModel>();
            var viewModel = new SalonsPaginatedListViewModel();
            if (sortId != null)
            {            
                var category = Mapper.Map<CategorySimpleViewModel>(_applicationDbContext.Categories.Where(x => x.Id == sortId).SingleOrDefault());         
                if (category == null)
                {
                    ViewBag.Exceptions = "No Salon Service Found...!";
                    return this.View("Error");
                }                
            }
            this.ViewData["CurrentFilter"] = sortId;
            int pageSize = 1;
            var pageIndex = pageNumber ?? 1;
            var salons = Mapper.Map<IEnumerable<SalonViewModel>>(GetAllWithPaging(sortId, pageSize, pageIndex));
            var salonsList = salons.ToList();
            var count = _applicationDbContext.Salons.Count();

            var list = new PaginatedList<SalonViewModel>(salonsList, count, pageIndex, pageSize);
            if (list.Any())
            {
                viewModel.Salons = list;
            }
            return this.View(viewModel);

        }
        private IEnumerable<Salon> GetAllWithPaging(int? sortId, int pageSize, int pageIndex)
        {
            IEnumerable<Salon> query = _applicationDbContext.Salons.OrderByDescending(x => x.CreatedOn);
            if (sortId != null)
            {
                query = query.Where(x => x.Id == sortId);
            }
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public ActionResult Details(string id)
        {

            Mapper.CreateMap<SalonService, SalonServiceSimpleViewModel>();
            
            var salonService = Mapper.Map<SalonServiceSimpleViewModel>(_applicationDbContext.SalonServices.Where(x => x.SalonId == id).SingleOrDefault());

            if (salonService == null || !salonService.Available)
            {
                ViewBag.Exceptions = "No Salon Service Found...!";
                return this.View("Error");
            }

            var viewModel = new SalonServiceSimpleViewModel
            {
                SalonId = id
            };
            return this.View(viewModel);

        }

    }
}