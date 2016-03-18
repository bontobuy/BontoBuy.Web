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
        [Display(Name = "Item Category")]
        [Required(ErrorMessage = "Item name is required")]
        public string Description { get; set; }
        public string AdminStatus { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        public IEnumerable<ModelViewModel> ItemModelNav { get; set; }

        [ForeignKey("ProductId")]
        public ProductViewModel ItemProductViewModel { get; set; }
    }
}