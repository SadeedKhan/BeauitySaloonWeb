using AutoMapper;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Categories;
using EmitMapper;
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
                //var list = new List<Category>();
                var viewModel = new CategoriesListViewModel();
                var cat =new IEnumerable<CategoryViewModel>();
                var list = from user in _applicationDbContext.Categories;
                if (list.Any())
                {
                   
                        viewModel.Categories = Mapper.Map<list, viewModel.Categories>(user);
                }

                if (list.Count > 0)
                {
                    var viewModel = new CategoriesListViewModel
                    {
                        Categories = Mapper.Map<list, Categories>,
                    };
                    return View(viewModel);
                }
                return View();
            }
            catch (Exception)
            {
                return null;
            }
          
            //var viewModel = new CategoriesListViewModel
            //{
            //    Categories=mapper.Map() _applicationDbContext.Categories,
            //};
        }
    }
}