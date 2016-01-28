using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ItemRepo : IItemRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ItemViewModel> Retrieve()
        {
            var records = db.Items.ToList();

            return records;
        }

        public ItemViewModel Get(int id)
        {
            var record = db.Items
                .Where(x => x.ItemId == id)
                .FirstOrDefault();

            return record;
        }

        public ItemViewModel Create(ItemViewModel item)
        {
            return null;
        }

        public ItemViewModel Update(int id, ItemViewModel item)
        {
            var currentrecord = db.Items
               .Where(x => x.ItemId == id)
               .FirstOrDefault();

            if (!(String.IsNullOrWhiteSpace(item.Description)) && !(String.IsNullOrWhiteSpace(item.TermsAndConditions)))
            {
                currentrecord.Description = item.Description;
                currentrecord.TermsAndConditions = item.TermsAndConditions;
                db.SaveChanges();

                return currentrecord;
            }

            return currentrecord;
        }

        public void Remove(int id)
        {
        }
    }
}