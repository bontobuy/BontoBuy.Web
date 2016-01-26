using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Product")]
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public IEnumerable<ItemViewModel> ProductItemNav { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryViewModel ProductCategoryNav { get; set; }
    }
}