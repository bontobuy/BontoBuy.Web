using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Specification")]
    public class SpecificationViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Specification Id")]
        public int SpecificationId { get; set; }
        [Display(Name = "SpecialCat Id")]
        public int SpecialCatId { get; set; }
        [Required(ErrorMessage = "Specification name is required")]
        [Display(Name = "Specification Name")]
        public string Description { get; set; }
        public string Status { get; set; }
        public IEnumerable<ModelSpecViewModel> ModelSpecNav { get; set; }
        [ForeignKey("SpecialCatId")]
        public SpecialCategoryViewModel SpecificationCategoryNav { get; set; }
    }

    public class SpecProductViewModel
    {
        public int ModelId { get; set; }
        public int SpecificationId { get; set; }
        public int SpecialCatId { get; set; }
        public string UserId { get; set; }
        public int SupplierId { get; set; }
        public int ItemId { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string ModelNumber { get; set; }
        public string Status { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0-9]{1,9}(?:\.[0-9]{1,2})?$", ErrorMessage = "Please enter a correct price!")]
        public int Price { get; set; }
        public string SpecDescription { get; set; }
        public string Value { get; set; }
        public HttpPostedFileBase File { get; set; }
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int NumberDaysToAdvert { get; set; }
        [Required]
        public int DeliveryInDays { get; set; }
    }

    public class SpecActionViewModel
    {
        public int SpecialCatId { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Specification name is required")]
        public string Description { get; set; }
    }

    public class ModelSpecificationDetails
    {
        public int ModelId { get; set; }
        public int SpecificationId { get; set; }
        public int SpecialCategoryId { get; set; }
        public string SpecificationValue { get; set; }
        public string SpecialCategoryName { get; set; }
        public string SpecificationName { get; set; }
        public string SpecialCategoryPosition { get; set; }
    }
}