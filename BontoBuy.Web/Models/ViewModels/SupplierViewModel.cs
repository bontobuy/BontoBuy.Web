using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Supplier")]
    public class SupplierViewModel : ApplicationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }
        public string Website { get; set; }

        public IEnumerable<ModelSpecViewModel> ModelSpecNav { get; set; }
    }
}