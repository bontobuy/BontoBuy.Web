using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Models
{
    [Table("Model")]
    public class ModelViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
        public int ItemId { get; set; }

        public IEnumerable<ModelSpecViewModel> ModeSpecNav { get; set; }

        [ForeignKey("ItemId")]
        public ItemViewModel ModelItemNav { get; set; }
    }

    public class ModelBrandViewModel
    {
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
    }
}