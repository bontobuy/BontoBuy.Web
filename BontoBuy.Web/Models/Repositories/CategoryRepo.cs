using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class CategoryRepo : ICategoryRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<CategoryViewModel> Retrieve()
        {
            var records = db.Categories.ToList();

            return records;
        }

        public CategoryViewModel Get(int id)
        {
            var record = db.Categories
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();

            return record;
        }

        public CategoryViewModel Create(CategoryViewModel item)
        {
            var newRecord = new CategoryViewModel
            {
                Description = item.Description
            };
            db.Categories.Add(newRecord);
            db.SaveChanges();

            return newRecord;
        }

        public CategoryViewModel Update(int id, CategoryViewModel item)
        {
            var currentrecord = db.Categories
                .Where(x => x.CategoryId == id)
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