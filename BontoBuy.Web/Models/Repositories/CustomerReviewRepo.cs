using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class CustomerReviewRepo : ICustomerReviewRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<ReviewViewModel> Retrieve(int modelId)
        {
            var records = db.Reviews.Where(x => x.ModelId == modelId).ToList();
            if (records == null)
                return null;

            return records;
        }
        public ReviewViewModel Create(ReviewViewModel item)
        {
            if (item == null)
                return null;

            db.Reviews.Add(item);
            db.SaveChanges();

            return item;
        }
        public bool VerifyReviewAbility(string userId, int modelId)
        {
            if (String.IsNullOrWhiteSpace(userId) || modelId < 1)
                return false;

            var reviews = db.Reviews.Where(x => x.UserId == userId && x.ModelId == modelId).Count();
            if (reviews > 0)
                return false;

            var order = (from o in db.Orders
                         where o.CustomerUserId == userId
                         && o.ModelId == modelId
                         select o).FirstOrDefault();

            if (order.OrderId < 1)
                return false;

            return true;
        }
    }
}