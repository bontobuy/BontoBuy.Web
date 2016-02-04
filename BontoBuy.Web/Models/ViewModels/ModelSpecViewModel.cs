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
        [Key, Column(Order = 0)]
        [Display(Name = "Specification Id")]
        public int SpecificationId { get; set; }
        [Display(Name = "Model Id")]
        public int ModelId { get; set; }
        [Display(Name = "Value")]
        [Required(ErrorMessage = "Specification value is required")]
        public string Value { get; set; }
        public string Description { get; set; }
        [ForeignKey("SpecificationId")]
        public SpecificationViewModel SpecificationNav { get; set; }
        [ForeignKey("ModelId")]
        public ModelViewModel ModelNav { get; set; }
    }
}