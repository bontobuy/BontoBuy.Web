using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class AdminRetrieveItemViewModel
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Product Type")]
        public string ProductDescription { get; set; }
        [Display(Name = "Item Name")]
        public string ItemDescription { get; set; }
        public string Status { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DtCreated { get; set; }
        [Display(Name = "Date Updated")]
        public DateTime DtUpdated { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }

    public class AdminGetItemViewModel
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Product Type")]
        public string ProductDescription { get; set; }
        [Display(Name = "Item Name")]
        public string ItemDescription { get; set; }
        public string Status { get; set; }
    }

    public class AdminCreateItemViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string ItemDescription { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
        public string Status { get; set; }
    }

    public class AdminUpdateItemViewModel
    {
        public int ItemId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string ItemDescription { get; set; }
        public DateTime DtUpdated { get; set; }
        public string Status { get; set; }
    }

    public class AdminRetrieveOrdersViewModel
    {
        [Display(Name = "Supplier Id")]
        public int SupplierId { get; set; }
        [Display(Name = "Order Id")]
        public int OrderId { get; set; }
        [Display(Name = "Model Id")]
        public int ModelId { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        public string Status { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DtCreated { get; set; }
        [Display(Name = "Commission Due")]
        public int SupplierCommission { get; set; }
        public int Price { get; set; }
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