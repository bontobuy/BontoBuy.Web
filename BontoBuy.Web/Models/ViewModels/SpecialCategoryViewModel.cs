using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("SpecialCategory")]
    public class SpecialCategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecialCatId { get; set; }
        [Display(Name = "Special Category")]
        [Required(ErrorMessage = "Special Category name is required")]
        public string Description { get; set; }
        public string Status { get; set; }
        public string Position { get; set; }
        public ICollection<SpecificationViewModel> TagSpecificationNav { get; set; }
    }
}