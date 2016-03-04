using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BontoBuy.Web.Models
{
    [Table("DeliveryAddress")]
    public class DeliveryAddressViewModel
    {
        [Key]
        public int DeliveryAddressId { get; set; }
        public string UserId { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Status { get; set; }
        [ForeignKey("UserId")]
        public CustomerViewModel CustomerNav { get; set; }
    }

    public class DeliveryAddressActionViewModel
    {
        public int DeliveryAddressStatusId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string UserId { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Status { get; set; }
    }
}