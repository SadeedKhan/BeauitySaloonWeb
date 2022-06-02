using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}