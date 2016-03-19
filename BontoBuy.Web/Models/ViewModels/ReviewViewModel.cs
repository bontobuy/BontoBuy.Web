using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Review")]
    public class ReviewViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        public int ModelId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }

        [ForeignKey("ModelId")]
        public ModelViewModel ModelNav { get; set; }
        [ForeignKey("UserId")]
        public CustomerViewModel CustomerNav { get; set; }
    }
}