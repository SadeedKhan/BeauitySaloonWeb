using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Categories
{
    public class CategoriesListViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}