using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BontoBuy.Web.Models
{
    [Table("Commission")]
    public class CommissionViewModel
    {
        [Key]
        public int CommissionId { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
    }

    public class CommissionOrderViewModel
    {
        public int OrderId { get; set; }
        public string ModelName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        public int Commission { get; set; }
        public bool Paid { get; set; }
    }
}