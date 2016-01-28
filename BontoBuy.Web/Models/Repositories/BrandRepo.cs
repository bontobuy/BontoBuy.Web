using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BontoBuy.Web.Models
{
    public class BrandRepo : IBrandRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<BrandViewModel> Retrieve()
        {
            var records = db.Brands.ToList();

            return records;
        }
        public BrandViewModel Get(int id)
        {
            var record = db.Brands
               .Where(x => x.BrandId == id)
               .FirstOrDefault();

            return record;
        }
        public BrandViewModel Create(BrandViewModel item)
        {
            var newRecord = new BrandViewModel
            {
                Name = item.Name
            };
            db.Brands.Add(newRecord);
            db.SaveChanges();

            return newRecord;
        }

        public BrandViewModel Update(int id, BrandViewModel item)
        {
            var currentrecord = db.Brands
                .Where(x => x.BrandId == id)
                .FirstOrDefault();

            if (!(String.IsNullOrWhiteSpace(item.Name)))
            {
                currentrecord.Name = item.Name;
            }
            db.SaveChanges();

            return currentrecord;
        }

        public void Remove(int id)
        {
        }
    }
}