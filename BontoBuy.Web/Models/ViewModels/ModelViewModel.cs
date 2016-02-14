using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Models
{
    [Table("Model")]
    public class ModelViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Model Id")]
        public int ModelId { get; set; }
        [Display(Name = "Brand Id")]
        public int BrandId { get; set; }
        [Display(Name = "Model Number")]
        [Required(ErrorMessage = "Model Number is required")]
        public string ModelNumber { get; set; }
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }

        [RegularExpression(@"^[+-]?[0-9]{1,9}(?:\.[0-9]{1,2})?$", ErrorMessage = "Please enter a correct price!")]

        //[Range(0, 99999, ErrorMessage = "Please enter a number")]
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        public string UserId { get; set; }
        public int SupplierId { get; set; }
        public string Status { get; set; }
        public IEnumerable<ModelSpecViewModel> ModeSpecNav { get; set; }

        [ForeignKey("ItemId")]
        [Column(Order = 0)]
        public ItemViewModel ModelItemNav { get; set; }
        [ForeignKey("BrandId")]
        [Column(Order = 1)]
        public BrandViewModel BrandNav { get; set; }
        [ForeignKey("UserId")]
        [Column(Order = 2)]
        public SupplierViewModel SupplierNav { get; set; }
    }

    public class ModelBrandViewModel
    {
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
    }
}