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
            var records = db.Products.ToList();

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
                Description = item.Description
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