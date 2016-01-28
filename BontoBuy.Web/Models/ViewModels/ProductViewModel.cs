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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        [Display(Name = "Sub Category Name")]
        public string Description { get; set; }

        public IEnumerable<ItemViewModel> ProductItemNav { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryViewModel ProductCategoryNav { get; set; }
    }
}