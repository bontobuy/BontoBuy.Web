﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BontoBuy.Web.Models
{
    [Table("Payment")]
    public class PaymentViewModel
    {
        [Key]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int CommissionId { get; set; }
        public int PaymentDate { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
        public int DiscountAllowed { get; set; }

        [ForeignKey("OrderId")]
        [Column(Order = 0)]
        public OrderViewModel OrderNav { get; set; }
        [ForeignKey("CommissionId")]
        [Column(Order = 1)]
        public CommissionViewModel CommissionNav { get; set; }
    }
}