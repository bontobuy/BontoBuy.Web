using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Item")]
    public class ItemViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [MaxLength]
        [Display(Name = "Item Category")]
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; }
        public IEnumerable<ModelViewModel> ItemModelNav { get; set; }

        [ForeignKey("ProductId")]
        public ProductViewModel ItemProductViewModel { get; set; }
    }
}