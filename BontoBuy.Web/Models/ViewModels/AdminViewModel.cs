using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Admin")]
    public class AdminViewModel : ApplicationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }
    }

    public class AdminModelStatusViewModel
    {
        public int ModelId { get; set; }
        public string Status { get; set; }
    }

    public class AdminRetrieveOrdersViewModel
    {
        public int OrderId { get; set; }
        public int ModelId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ModelNumber { get; set; }
        public string SupplierName { get; set; }
        public string Status { get; set; }
        public DateTime DtCreated { get; set; }
    }

    public class AdminGetOrderViewModel
    {
        public int OrderId { get; set; }
        public string SupplierName { get; set; }
        public string ModelNumber { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
        public int GrandTotal { get; set; }
        public int Quantity { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime RealDeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerFName { get; set; }
        public string CustomerLName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public int UnitPrice { get; set; }
    }
}