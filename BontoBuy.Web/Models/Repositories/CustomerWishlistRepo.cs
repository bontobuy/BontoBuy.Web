using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models.Repositories
{
    public class CustomerWishlistRepo : ICustomerWishlistRepo
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<WishlistViewModel> Retrieve(string id)
        {
            var records = db.Wishlists.Where(x => x.UserId == id).ToList();
            if (records == null)
                return null;

            return records;
        }
        public IEnumerable<CustomerRetrieveWishlistViewModel> RetrieveModels(int id)
        {
            var records = db.WishlistModels.Where(x => x.WishlistId == id).ToList();
            if (records == null)
                return null;

            var itemList = AssignModelListToWishlist(records);
            if (itemList == null)
                return null;

            return itemList;
        }

        public WishlistModelViewModel AddToWishlist(int id)
        {
            if (id < 1)
                return null;

            var record = db.Wishlists.Where(x => x.Status == "Default").FirstOrDefault();
            if (record == null)
                return null;

            var itemToAdd = CreateWishlistModel(record, id);
            if (itemToAdd == null)
                return null;

            return itemToAdd;
        }

        public WishlistModelViewModel RemoveFromWishlist(int id)
        {
            if (id < 1)
                return null;

            var record = db.WishlistModels.Where(x => x.WishlistModelId == id).FirstOrDefault();
            if (record == null)
                return null;

            db.WishlistModels.Remove(record);
            db.SaveChanges();

            return record;
        }

        private IEnumerable<CustomerRetrieveWishlistViewModel> AssignModelListToWishlist(List<WishlistModelViewModel> records)
        {
            if (records == null)
                return null;

            var itemList = new List<CustomerRetrieveWishlistViewModel>();

            foreach (var item in records)
            {
                var listItem = new CustomerRetrieveWishlistViewModel()
                {
                    ModelId = item.ModelId,
                    Name = (from m in db.Models
                            where m.ModelId == item.ModelId
                            select m.ModelNumber).FirstOrDefault(),
                    WishlistId = item.WishlistId,
                    Price = (from m in db.Models
                             where m.ModelId == item.ModelId
                             select m.Price).FirstOrDefault()
                };
                itemList.Add(listItem);
            }
            return itemList;
        }

        private WishlistModelViewModel CreateWishlistModel(WishlistViewModel record, int modelId)
        {
            var itemToAdd = new WishlistModelViewModel()
            {
                WishlistId = record.WishlistId,
                ModelId = modelId
            };

            db.WishlistModels.Add(itemToAdd);
            db.SaveChanges();

            return itemToAdd;
        }
    }
}