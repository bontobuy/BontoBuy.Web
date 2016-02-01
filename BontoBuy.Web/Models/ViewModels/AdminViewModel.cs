using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Admin")]
    public class AdminViewModel : ApplicationUser
    {
        public int AdminId { get; set; }
    }
}