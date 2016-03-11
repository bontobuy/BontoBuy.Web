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
}