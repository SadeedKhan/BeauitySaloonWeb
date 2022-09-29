using AutoMapper;
using BeauitySaloonWeb.Areas.Administration.Controllers.Base;
using BeauitySaloonWeb.CustomsValidations;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminCategoriesController : AdministrationController
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

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

        public ActionResult AddCategory()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string imageUrl;
            try
            {
                var allowedExtensions = new[] {".Jpg", ".png", ".jpg", ".jpeg"};
                var ext = Path.GetExtension(input.file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    imageUrl = UploadPicture.WriteFile(input.file, "Categories");
                    _applicationDbContext.Categories.Add(new Category
                    {
                        Name = input.Name,
                        Description = input.Description,
                        ImageUrl = imageUrl,
                        CreatedOn = DateTime.Now
                    });
                    _applicationDbContext.SaveChanges();
                    return this.RedirectToAction("Index");
                   
                }
                else
                {
                    ViewBag.message = "Please choose only Image file";
                    return View(input);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("AddCategory");
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            if (id <= 6)
            {
                return this.RedirectToAction("Index");
            }
            var obj = _applicationDbContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _applicationDbContext.Categories.Remove(obj);
                _applicationDbContext.SaveChanges();
            }
            return this.RedirectToAction("Index");
        }
    }
}