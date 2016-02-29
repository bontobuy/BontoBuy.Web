using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class OrderStatusViewModel
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string Name { get; set; }
    }
}