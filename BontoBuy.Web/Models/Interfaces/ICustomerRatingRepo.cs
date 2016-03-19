using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface ICustomerRatingRepo
    {
        int GetModelRating(int id);
        RatingModelViewModel Create(RatingModelViewModel item);
    }
}