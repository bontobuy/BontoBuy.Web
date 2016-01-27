using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Model")]
    public class ModelViewModel
    {
        [Key]
        public int ModelId { get; set; }
        public int ItemId { get; set; }
        public int Price { get; set; }

        public IEnumerable<ModelSpecViewModel> ModeSpecNav { get; set; }

        [ForeignKey("ItemId")]
        public ItemViewModel ModelItemNav { get; set; }
    }
}