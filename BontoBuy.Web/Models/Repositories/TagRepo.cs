using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BontoBuy.Web.HelperMethods;

namespace BontoBuy.Web.Models
{
    public class TagRepo : ITagRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Helper helper = new Helper();
        public IEnumerable<SpecialCategoryViewModel> Retrieve()
        {
            var records = db.SpecialCategories.ToList().OrderBy(x => x.Description);

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
            if (String.IsNullOrEmpty(item.Description))
                return null;

            SpecialCategoryViewModel existingRecord = null;

            string descriptionTitleCase = helper.ConvertToTitleCase(item.Description);
            existingRecord = CheckForDuplicates(descriptionTitleCase);
            if (existingRecord == null)
            {
                var newRecord = new SpecialCategoryViewModel
                {
                    Description = descriptionTitleCase
                };

                db.SpecialCategories.Add(newRecord);
                db.SaveChanges();

                return newRecord;
            }

            return existingRecord;
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

        private SpecialCategoryViewModel CheckForDuplicates(string description)
        {
            if (String.IsNullOrEmpty(description))
                return null;

            var record = db.SpecialCategories.Where(x => x.Description == description).FirstOrDefault();
            if (record != null)
                return record;

            return null;
        }
    }
}