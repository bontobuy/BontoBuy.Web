using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("DeliveryAddressStatus")]
    public class DeliveryAddressStatusViewModel
    {
        [Key]
        public int DeliveryAddressStatusId { get; set; }
        public string Status { get; set; }
    }
}