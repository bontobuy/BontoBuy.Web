using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class SpecificationRepo : ISpecificationRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<SpecificationViewModel> Retrieve()
        {
            var records = db.Specifications.ToList();

            return records;
        }
        public SpecificationViewModel Get(int id)
        {
            var record = db.Specifications
               .Where(x => x.SpecificationId == id)
               .FirstOrDefault();

            return record;
        }
        public SpecificationViewModel Create(SpecificationViewModel item)
        {
            var newRecord = new SpecificationViewModel
            {
                Description = item.Description
            };
            db.Specifications.Add(newRecord);
            db.SaveChanges();
            return newRecord;
        }

        public SpecificationViewModel Update(int id, SpecificationViewModel item)
        {
            var currentrecord = db.Specifications
                .Where(x => x.SpecificationId == id)
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

        //public IEnumerable<> GetList()
        //{
        //    var records = from r in db.Categories
        //                  select new { r.CategoryId, r.Description };

        //    return records;
        //}
    }
}