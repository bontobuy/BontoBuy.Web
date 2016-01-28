using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ProductCreationViewModel
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}