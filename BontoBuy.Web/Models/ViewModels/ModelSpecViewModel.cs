using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ModelSpecViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelSpecId { get; set; }
        [Display(Name = "Specification Id")]
        [Column(Order = 0)]
        public int SpecificationId { get; set; }
        [Display(Name = "Model Id")]
        [Column(Order = 1)]
        public int ModelId { get; set; }
        [Column(Order = 3)]
        public string UserId { get; set; }
        public int SupplierId { get; set; }
        [Display(Name = "Value")]
        [Required(ErrorMessage = "Specification value is required")]
        public string Value { get; set; }

        [ForeignKey("UserId")]
        public SupplierViewModel SupplierNav { get; set; }
        [ForeignKey("SpecificationId")]
        public SpecificationViewModel SpecificationNav { get; set; }
        [ForeignKey("ModelId")]
        public ModelViewModel ModelNav { get; set; }
    }

    public class ModelSpecCreationViewModel
    {
        public int ModelSpecId { get; set; }
        public int SpecificationId { get; set; }
        public int ModelId { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public class ModelToCartViewModel
    {
        public int ModelId { get; set; }
        public int SupplierId { get; set; }
    }
}