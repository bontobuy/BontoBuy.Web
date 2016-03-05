using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BontoBuy.Web.Models
{
    [Table("Rating")]
    public class RatingViewModel
    {
        [Key]
        public int RatingId { get; set; }

        //Need to add limit 1-5
        public int RatingNumber { get; set; }

        public IEnumerable<ModelViewModel> ModelNav { get; set; }
    }
}