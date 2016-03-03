using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Wishlist")]
    public class WishlistViewModel
    {
        [Key]
        [Column(Order = 0)]
        public int ModelId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishlistId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public IEnumerable<ModelViewModel> ModelNav { get; set; }
        public IEnumerable<CustomerViewModel> CustomerNav { get; set; }
    }
}