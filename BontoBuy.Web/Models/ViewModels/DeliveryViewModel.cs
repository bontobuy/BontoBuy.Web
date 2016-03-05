using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Delivery")]
    public class DeliveryViewModel
    {
        [Key]
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime ActualDeliveryDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Status { get; set; }
        [ForeignKey("OrderId")]
        public OrderViewModel OrderNav { get; set; }
    }
}