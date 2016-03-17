using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BontoBuy.Web.Models
{
    [Table("WishlistModel")]
    public class WishlistModelViewModel
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishlistModelId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int WishlistId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ModelId { get; set; }

        public IEnumerable<WishlistViewModel> WishlistNav { get; set; }
        public IEnumerable<ModelViewModel> ModelNav { get; set; }
    }

    public class CustomerRetrieveWishlistViewModel
    {
        public int ModelId { get; set; }
        public int WishlistId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}