using AutoMapper;
using BeauitySaloonWeb.Areas.Administration.Controllers.Base;
using BeauitySaloonWeb.CustomsValidations;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.BlogPosts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminBlogPostsController : AdministrationController
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        public  ActionResult Index()
        {
            Mapper.CreateMap<BlogPost, BlogPostViewModel>();
            var viewModel = new BlogPostsListViewModel();
            var list = _applicationDbContext.BlogPosts.AsEnumerable().OrderByDescending(x => x.CreatedOn).ToList();
            if (list != null && list.Any())
            {
                viewModel.BlogPosts = Mapper.Map<IEnumerable<BlogPostViewModel>>(list); 
            }
            return this.View(viewModel);
        }

        public ActionResult AddBlogPost()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddBlogPost(BlogPostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string imageUrl;
            try
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                var ext = Path.GetExtension(input.file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    imageUrl = UploadPicture.WriteFile(input.file, "BlogPosts");
                    _applicationDbContext.BlogPosts.Add(new BlogPost
                    {

                        Title = input.Title,
                        Content = input.Content,
                        Author = input.Author,
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
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult DeleteBlogPost(int id)
        {
            if (id <= 4)
            {
                return this.RedirectToAction("Index");
            }
            var obj=_applicationDbContext.BlogPosts.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _applicationDbContext.BlogPosts.Remove(obj);
                _applicationDbContext.SaveChanges();
            }
            return this.RedirectToAction("Index");
        }
    }
}