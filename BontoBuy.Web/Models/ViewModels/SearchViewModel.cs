using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class SearchViewModel
    {
    }

    public class SearchFilter
    {
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
    }

    public class SearchResultViewModel
    {
        public int ModelId { get; set; }
    }
}