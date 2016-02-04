using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Photo")]
    public class PhotoViewModel
    {
        [Key]
        public int PhotoId { get; set; }
        [Required]
        public string ImagePath { get; set; }
    }
}