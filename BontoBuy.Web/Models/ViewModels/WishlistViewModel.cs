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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishlistId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
        public string Status { get; set; }

        [ForeignKey("UserId")]
        public CustomerViewModel CustomerNav { get; set; }
    }

    //public class CustomerRetrieveWishlistViewModel
    //{
    //    public int ModelId { get; set; }
    //    public string UserId { get; set; }
    //    public int WishlistId { get; set; }
    //    public string Name { get; set; }
    //}

    //public class CustomerGetWishlistViewModel
    //{
    //    public int ModelId { get; set; }
    //    public string ModelNumber { get; set; }
    //    public int Price { get; set; }
    //    public string UserId { get; set; }
    //    public int WishlistId { get; set; }
    //    public string Name { get; set; }
    //}

    //public class CustomerCreateWishlistViewModel
    //{
    //    public int ModelId { get; set; }
    //    public string UserId { get; set; }
    //    public string Name { get; set; }
    //}

    //public class CustomerUpdateWishlistViewModel
    //{
    //    public int ModelId { get; set; }
    //    public string UserId { get; set; }
    //    public int WishlistId { get; set; }
    //    public string Name { get; set; }
    //}

    public class CustomerArchiveWishlistViewModel
    {
    }
}