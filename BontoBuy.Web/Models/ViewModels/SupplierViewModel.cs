using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Supplier")]
    public class SupplierViewModel : ApplicationUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }
        public string Website { get; set; }

        public IEnumerable<ModelSpecViewModel> ModelSpecNav { get; set; }
        public IEnumerable<OrderViewModel> OrderNav { get; set; }
    }

    public class SupplierRetrieveModelsViewModel
    {
        public int SupplierId { get; set; }
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
        public string BrandName { get; set; }
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string ImageUrl { get; set; }
        public string PhysicalPath { get; set; }
    }

    public class SupplierGetModelSpecViewModel
    {
        public int SpecificationId { get; set; }
        public int SupplierId { get; set; }
        public int ModelId { get; set; }
        public string Value { get; set; }
        public int ModelSpecId { get; set; }
        public string Description { get; set; }
    }

    public class SupplierRetrieveOrdersViewModel
    {
        public int OrderId { get; set; }
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public DateTime DtCreated { get; set; }
    }

    public class SupplierGetOrderViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ModelNumber { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
        public int GrandTotal { get; set; }
        public int Quantity { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime RealDeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class SupplierUpdateOrderViewModel
    {
        public int OrderStatusId { get; set; }
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ModelNumber { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
        public int GrandTotal { get; set; }
        public int Quantity { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime RealDeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
    }
}