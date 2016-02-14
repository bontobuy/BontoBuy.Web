using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class CartViewModel
    {
        public int SupplierId { get; set; }
        public int ModelId { get; set; }
        public string UserId { get; set; }
        public string ModelName { get; set; }
        public string ImageUrl { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0-9]{1,9}(?:\.[0-9]{1,2})?$", ErrorMessage = "Please enter a correct quantity!")]
        public int Quantity { get; set; }

        //  public int CustomerId { get; set; }
    }

    public class CartDisplayViewModel
    {
        public string UserId { get; set; }
        public string ModelName { get; set; }
    }
}