using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Supplier")]
    public class SupplierViewModel : ApplicationUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }
        public string Website { get; set; }

        public IEnumerable<ModelSpecViewModel> ModelSpecNav { get; set; }
    }

    public class SupplierRetrieveModelsViewModel
    {
        public int SupplierId { get; set; }
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
        public string BrandName { get; set; }
    }

    public class SupplierGetModelSpecViewModel
    {
        public int SpecificationId { get; set; }
        public int SupplierId { get; set; }
        public int ModelId { get; set; }
        public string Value { get; set; }
        public int ModelSpecId { get; set; }
        public string Description { get; set; }
    }
}