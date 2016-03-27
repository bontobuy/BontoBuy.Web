using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Return")]
    public class ReturnViewModel
    {
        [Key]
        public int ReturnId { get; set; }
        public int OrderId { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public string ReturnMethod { get; set; }
        [Required]
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
        public string Notification { get; set; }
        [ForeignKey("OrderId")]
        public OrderViewModel OrderNav { get; set; }
    }

    public class ReturnActionViewModel
    {
        public int ReturnStatusId { get; set; }
        public int ReturnId { get; set; }
        public int OrderId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnMethod { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
    }
}