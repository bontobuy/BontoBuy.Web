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
        public int TagId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }

    public class SpecActionViewModel
    {
        public int SpecialCatId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
    }
}