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
        public int Percentage { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }

        public IEnumerable<PaymentViewModel> PaymentNav { get; set; }
    }
}