using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class TagRepo : ITagRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<TagViewModel> Retrieve()
        {
            var records = db.Tags.ToList();

            return records;
        }
        public TagViewModel Get(int id)
        {
            var record = db.Tags
               .Where(x => x.TagId == id)
               .FirstOrDefault();

            return record;
        }
        public TagViewModel Create(TagViewModel item)
        {
            var newRecord = new TagViewModel
            {
                Description = item.Description
            };
            db.Tags.Add(newRecord);
            db.SaveChanges();

            return newRecord;
        }

        public TagViewModel Update(int id, TagViewModel item)
        {
            var currentrecord = db.Tags
                .Where(x => x.TagId == id)
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