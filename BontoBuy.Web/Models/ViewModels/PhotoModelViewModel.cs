using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("PhotoModel")]
    public class PhotoModelViewModel
    {
        [Key]
        [Column(Order = 0)]
        public int PhotoId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ModelId { get; set; }

        public IEnumerable<PhotoViewModel> PhotoNav { get; set; }
        public IEnumerable<ModelViewModel> ModelNav { get; set; }
    }
}