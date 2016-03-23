using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BontoBuy.Web.HelperMethods;

namespace BontoBuy.Web.Models
{
    public class SpecificationRepo : ISpecificationRepo
    {
        private Helper helper = new Helper();
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<SpecificationViewModel> Retrieve()
        {
            var records = (from specs in db.Specifications
                           where specs.Status == "Active"
                           select specs).ToList().OrderBy(x => x.Description);

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
            string stringTitleCase = helper.ConvertToTitleCase(item.Description);
            var existingItem = CheckForDuplicates(stringTitleCase);
            if (existingItem == null)
            {
                var newRecord = new SpecificationViewModel
                {
                    Description = stringTitleCase,
                    Status = "Active"
                };

                db.Specifications.Add(newRecord);
                db.SaveChanges();
                return newRecord;
            }

            return null;
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

        private SpecificationViewModel CheckForDuplicates(string description)
        {
            if (String.IsNullOrEmpty(description))
                return null;

            var record = db.Specifications.Where(x => x.Description == description).FirstOrDefault();
            if (record != null)
                return record;

            return null;
        }
    }
}