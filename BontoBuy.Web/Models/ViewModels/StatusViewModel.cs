using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class StatusViewModel
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }
    }
}