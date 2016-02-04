using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class SupplierViewModel : ApplicationUser
    {
        public int SupplierId { get; set; }
        public string Website { get; set; }
    }
}