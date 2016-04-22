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
        public string ModelName { get; set; }
        public string SupplierId { get; set; }
    }

    public class SearchResultViewModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DtCreated { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ItemName { get; set; }
        public string Rating { get; set; }
    }
}