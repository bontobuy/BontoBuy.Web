using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("PaymentStatus")]
    public class PaymentStatusViewModel
    {
        [Key]
        public int PaymentStatusId { get; set; }
        public string Status { get; set; }
    }
}