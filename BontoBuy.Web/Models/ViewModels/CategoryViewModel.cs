using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Category")]
    public class CategoryViewModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public IEnumerable<ProductViewModel> CategoryProductNav { get; set; }
    }
}