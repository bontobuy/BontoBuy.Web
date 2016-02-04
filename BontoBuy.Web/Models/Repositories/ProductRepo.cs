using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ProductRepo : IProductRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ProductViewModel> Retrieve()
        {
            var records = (from products in db.Products
                           where products.Status == "Active"
                           select products).ToList();

            return records;
        }
        public ProductViewModel Get(int id)
        {
            var record = db.Products
               .Where(x => x.ProductId == id)
               .FirstOrDefault();

            return record;
        }
        public ProductViewModel Create(ProductViewModel item)
        {
            var newRecord = new ProductViewModel
            {
                Description = item.Description,
                Status = "Active"
            };
            db.Products.Add(newRecord);
            db.SaveChanges();
            return newRecord;
        }

        public ProductViewModel Update(int id, ProductViewModel item)
        {
            var currentrecord = db.Products
                .Where(x => x.ProductId == id)
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
            var record = db.Products
                .Where(x => x.ProductId == id && x.Status == "Active")
                .FirstOrDefault();

            if (record != null)
            {
                record.Status = "Archived";
                db.SaveChanges();
            }
        }

        public IEnumerable<ProductViewModel> RetrieveArchives()
        {
            var records = (from products in db.Products
                           where products.Status == "Archived"
                           select products).ToList();

            return records;
        }

        public void RevertArchive(int id)
        {
            var record = db.Products
                .Where(x => x.ProductId == id && x.Status == "Archived")
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