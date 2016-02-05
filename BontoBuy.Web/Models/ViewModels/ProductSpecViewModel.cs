using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("ProductSpec")]
    public class ProductSpecViewModel
    {
        [Key]
        [Column(Order = 0)]
        public int ProductId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int SpecificationId { get; set; }
        public IEnumerable<ProductViewModel> ProductNav { get; set; }
        public IEnumerable<SpecificationViewModel> SpecNav { get; set; }
    }
}