using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class CustomerRatingRepo : ICustomerRatingRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int GetModelRating(int id)
        {
            var records = db.RatingModels.Where(x => x.ModelId == id).ToList();

            var rating = CalculateRating(records);

            return rating;
        }

        public RatingModelViewModel Create(RatingModelViewModel item)
        {
            if (item == null)
                return null;

            db.RatingModels.Add(item);
            db.SaveChanges();

            return item;
        }

        private int CalculateRating(List<RatingModelViewModel> items)
        {
            int numberOfRatings = items.Count();
            int numRatingOne = items.Count(x => x.RatingId == 1);
            int numRatingTwo = items.Count(x => x.RatingId == 2);
            int numRatingThree = items.Count(x => x.RatingId == 3);
            int numRatingFour = items.Count(x => x.RatingId == 4);
            int numRatingFive = items.Count(x => x.RatingId == 5);

            int avgRating = Convert.ToInt32(((numRatingOne * 1) + (numRatingTwo * 2) + (numRatingThree * 3) +
                (numRatingFour * 4) + (numRatingFive * 5)) / numberOfRatings);

            return avgRating;
        }
    }
}