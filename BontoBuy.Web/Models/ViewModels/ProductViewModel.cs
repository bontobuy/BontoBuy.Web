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
        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Category Name")]
        public string Description { get; set; }
        public string Status { get; set; }
        public IEnumerable<ItemViewModel> ProductItemNav { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryViewModel ProductCategoryNav { get; set; }
    }

    public class AdminRetrieveProduct
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}