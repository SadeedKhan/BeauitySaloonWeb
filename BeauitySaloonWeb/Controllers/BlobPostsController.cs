using AutoMapper;
using BeauitySaloonWeb.CustomsValidations;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.BlogPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Controllers
{
    public class BlobPostsController : Controller
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        // GET: BlobPosts
        public ActionResult Index(
             int? sortId,
             int? pageNumber) // blogPostId
        {
            Mapper.CreateMap<BlogPost, BlogPostViewModel>();
            if (sortId != null)
            {
                var blogPost = _applicationDbContext.BlogPosts.Where(x => x.Id == sortId).FirstOrDefault();
                if (blogPost == null)
                {
                    ViewBag.Exception = "No BlobPost Found...!";
                    return View("Error"); ;
                }
            }
            this.ViewData["CurrentSort"] = sortId;
            int pageSize = 1;
            var pageIndex = pageNumber ?? 1;
            var viewModel = new BlogPostsPaginatedListViewModel();
            var blogPosts = Mapper.Map<IEnumerable<BlogPostViewModel>>(GetAllWithPaging(sortId, pageSize, pageIndex).ToList());
            var blogPostsList = blogPosts.ToList();
            var count = _applicationDbContext.BlogPosts.Count();
            var list = new PaginatedList<BlogPostViewModel>(blogPostsList, count, pageIndex, pageSize);
            if (list.Any())
            {
                viewModel.BlogPosts = list;
            }
            return this.View(viewModel);
        }


        private IEnumerable<BlogPost> GetAllWithPaging(int? sortId,int pageSize,int pageIndex)
        {
            IEnumerable<BlogPost> query =_applicationDbContext.BlogPosts.OrderByDescending(x => x.CreatedOn);
            if (sortId != null)
            {
                query = query.Where(x => x.Id == sortId);
            }
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}