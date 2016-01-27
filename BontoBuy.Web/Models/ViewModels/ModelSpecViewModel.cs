using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ModelSpecViewModel
    {
        [Key, Column(Order = 0)]
        public int SpecificationId { get; set; }
        [Key, Column(Order = 1)]
        public int ModelId { get; set; }
        public string Value { get; set; }
        [ForeignKey("SpecificationId")]
        public SpecificationViewModel SpecificationNav { get; set; }
        [ForeignKey("ModelId")]
        public ModelViewModel ModelNav { get; set; }
    }
}