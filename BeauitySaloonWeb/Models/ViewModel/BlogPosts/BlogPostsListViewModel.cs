using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.BlogPosts
{
    public class BlogPostsListViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }
    }
}