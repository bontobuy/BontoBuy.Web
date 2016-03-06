using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Order")]
    public class OrderViewModel
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime RealDeliveryDate { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
        public int GrandTotal { get; set; }
        public int SupplierId { get; set; }
        public int CustomerId { get; set; }
        public int ModelId { get; set; }
        public string CustomerUserId { get; set; }
        public string SupplierUserId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public IEnumerable<DeliveryViewModel> DeliveryNav { get; set; }
        public IEnumerable<PaymentViewModel> PaymentNav { get; set; }
        [ForeignKey("SupplierUserId")]
        [Column(Order = 0)]
        public SupplierViewModel SupplierNav { get; set; }
        [ForeignKey("CustomerUserId")]
        [Column(Order = 1)]
        public CustomerViewModel CustomerNav { get; set; }
        [ForeignKey("ModelId")]
        [Column(Order = 2)]
        public ModelViewModel ModelNav { get; set; }
    }
}