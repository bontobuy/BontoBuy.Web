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
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        [MaxLength]
        public string Description { get; set; }
        public string TermsAndConditions { get; set; }

        public IEnumerable<ModelViewModel> ItemModelNav { get; set; }

        [ForeignKey("ProductId")]
        public ProductViewModel ItemProductViewModel { get; set; }
    }
}