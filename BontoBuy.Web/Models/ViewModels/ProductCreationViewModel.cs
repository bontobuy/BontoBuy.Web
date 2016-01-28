using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ProductCreationViewModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
    }
}