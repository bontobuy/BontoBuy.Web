using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class HomeCatalogViewModel
    {
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int Price { get; set; }
        public int SpecificationId { get; set; }
        public string SpecificationDescription { get; set; }
        public string SpecificationValue { get; set; }
        public string ImageUrl { get; set; }
        public string PhysicalPath { get; set; }
    }
}