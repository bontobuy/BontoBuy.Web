using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("RatingModel")]
    public class RatingModelViewModel
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingModelId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ModelId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int RatingId { get; set; }

        public IEnumerable<RatingViewModel> RatingNav { get; set; }
        public IEnumerable<ModelViewModel> ModelNav { get; set; }
    }
}