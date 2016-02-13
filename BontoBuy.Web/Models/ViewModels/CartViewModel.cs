using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class CartViewModel
    {
        public int SpecificationId { get; set; }
        public int SupplierId { get; set; }
        public int ModelId { get; set; }
        public string Value { get; set; }
        public int ModelSpecId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        //  public int CustomerId { get; set; }
    }

    public class CartDisplayViewModel
    {
        public string UserId { get; set; }
        public string ModelName { get; set; }
    }
}