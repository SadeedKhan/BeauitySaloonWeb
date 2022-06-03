using AutoMapper;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.BlogPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index()
        {

            try
            {
                var viewModel = new BlogPostsListViewModel();

                IEnumerable<BlogPost> list = _applicationDbContext.BlogPosts.ToList();

                Mapper.CreateMap<BlogPost, BlogPostViewModel>();
                if (list.Any())
                {
                    viewModel.BlogPosts = Mapper.Map<IEnumerable<BlogPostViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
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