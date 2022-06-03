using AutoMapper;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        // GET: Categories

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
    }
}