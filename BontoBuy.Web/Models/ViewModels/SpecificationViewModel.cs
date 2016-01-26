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
        public SpecificationViewModel()
        {
            SpecificationModelNav = new HashSet<ModelViewModel>();
        }

        [Key]
        public int SpecificationId { get; set; }

        public ICollection<ModelViewModel> SpecificationModelNav { get; set; }
    }
}