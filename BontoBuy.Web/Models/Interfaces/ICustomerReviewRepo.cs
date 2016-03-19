using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface ICustomerReviewRepo
    {
        List<ReviewViewModel> Retrieve(int modelId);
        bool VerifyReviewAbility(string userId, int modelId);
        ReviewViewModel Create(ReviewViewModel item);
    }
}