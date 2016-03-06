using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("ReturnStatus")]
    public class ReturnStatusViewModel
    {
        [Key]
        public int ReturnStatusId { get; set; }
        public string Status { get; set; }
    }
}