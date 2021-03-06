﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Brand")]
    public class BrandViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Brand Id")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Brand Name is required")]
        [Display(Name = "Brand Name")]
        public string Name { get; set; }
        public string Status { get; set; }
        public IEnumerable<ModelViewModel> ModelNav { get; set; }
    }
}