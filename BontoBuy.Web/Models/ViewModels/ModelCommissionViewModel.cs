using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("ModelCommission")]
    public class ModelCommissionViewModel
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelCommissionId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ModelId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CommissionId { get; set; }

        public ModelViewModel ModelNav { get; set; }
        public CommissionViewModel CommissionNav { get; set; }
    }
}