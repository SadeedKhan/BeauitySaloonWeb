﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Categories
{
    public class CategoriesSimpleListViewModel
    {
        public IEnumerable<CategorySimpleViewModel> Categories { get; set; }
    }
}