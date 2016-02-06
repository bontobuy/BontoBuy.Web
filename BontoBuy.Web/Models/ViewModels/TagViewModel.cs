using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("SpecialCategory")]
    public class TagViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        [Display(Name = "Tag Name")]
        [Required(ErrorMessage = "Tag name is required")]
        public string Description { get; set; }
        public string Status { get; set; }
        public ICollection<SpecificationViewModel> TagSpecificationNav { get; set; }
    }
}