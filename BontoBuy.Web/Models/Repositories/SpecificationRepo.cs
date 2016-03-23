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
            var records = (from specs in db.Specifications
                           where specs.Status == "Active"
                           select specs).ToList();

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
                Description = item.Description,
                Status = "Active"
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

        public void Archive(int id)
        {
            var record = db.Specifications
                .Where(x => x.SpecificationId == id && x.Status == "Active")
                .FirstOrDefault();

            if (record != null)
            {
                record.Status = "Archived";
                db.SaveChanges();
            }
        }

        public IEnumerable<SpecificationViewModel> RetrieveArchives()
        {
            var records = (from specifications in db.Specifications
                           where specifications.Status == "Archived"
                           select specifications).ToList();

            return records;
        }

        public void RevertArchive(int id)
        {
            var record = db.Specifications
                .Where(x => x.SpecificationId == id && x.Status == "Archived")
                .FirstOrDefault();

            if (record != null)
            {
                record.Status = "Active";
                db.SaveChanges();
            }
        }

        //public IEnumerable<> GetList()
        //{
        //    var records = from r in db.Categories
        //                  select new { r.CategoryId, r.Description };

        //    return records;
        //}
    }
}