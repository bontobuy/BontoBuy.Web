﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Item")]
    public class ItemViewModel
    {
        [Key]
        public int ItemId { get; set; }
        public string Description { get; set; }

        public ProductViewModel ItemProductViewModel { get; set; }
    }
}