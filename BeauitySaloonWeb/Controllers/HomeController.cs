using AutoMapper;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            try
            {
                var viewModel = new CategoriesListViewModel();
                IEnumerable<Category> list = _applicationDbContext.Categories.ToList();
                Mapper.CreateMap<Category, CategoryViewModel>();
                if (list.Any())
                {
                    viewModel.Categories = Mapper.Map<IEnumerable<CategoryViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }

        public ActionResult Privacy()
        {
            return View();
        }

        [Route("/Home/Error/404")]
        public ActionResult Error404()
        {
            return View();
        }

        //[Route("/Error")]
        //public ActionResult Error()
        //{
        //    return View();
        //}
    }
}