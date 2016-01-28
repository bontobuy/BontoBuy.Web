using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ProductCreationRepo : IProductCreationRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ProductCreationViewModel> Retrieve()
        {
            return null;
        }
        public ProductCreationViewModel Get(int id)
        {
            return null;
        }
        public ProductCreationViewModel Create(ProductCreationViewModel item)
        {
            return null;
        }
        public ProductCreationViewModel Update(int id, ProductCreationViewModel item)
        {
            return null;
        }

        public IEnumerable<ProductViewModel> RetrieveProductByCategory(ProductViewModel item)
        {
            int categoryId = item.CategoryId;

            var records = from products in db.Products
                          where products.CategoryId == categoryId
                          select products;

            return records;
        }

        public ProductCreationViewModel AssignProductValue(ProductViewModel item)
        {
            var productTransfer = new ProductCreationViewModel()
            {
                ProductId = item.ProductId,
                CategoryId = item.CategoryId,
                Description = item.Description,
            };

            return productTransfer;
        }
    }
}