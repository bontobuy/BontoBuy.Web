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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string Description { get; set; }
        public string Status { get; set; }
        public IEnumerable<ProductViewModel> CategoryProductNav { get; set; }
    }

    public class CatViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string Description { get; set; }
    }
}