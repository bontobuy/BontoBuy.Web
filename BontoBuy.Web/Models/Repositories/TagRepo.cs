using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class TagRepo : ITagRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<SpecialCategoryViewModel> Retrieve()
        {
            var records = db.SpecialCategories.ToList();

            return records;
        }
        public SpecialCategoryViewModel Get(int id)
        {
            var record = db.SpecialCategories
               .Where(x => x.SpecialCatId == id)
               .FirstOrDefault();

            return record;
        }
        public SpecialCategoryViewModel Create(SpecialCategoryViewModel item)
        {
            var newRecord = new SpecialCategoryViewModel
            {
                Description = item.Description
            };
            db.SpecialCategories.Add(newRecord);
            db.SaveChanges();

            return newRecord;
        }

        public SpecialCategoryViewModel Update(int id, SpecialCategoryViewModel item)
        {
            var currentrecord = db.SpecialCategories
                .Where(x => x.SpecialCatId == id)
                .FirstOrDefault();

            if (!(String.IsNullOrWhiteSpace(item.Description)))
            {
                currentrecord.Description = item.Description;
            }
            db.SaveChanges();

            return currentrecord;
        }

        public void Remove(int id)
        {
        }
    }
}