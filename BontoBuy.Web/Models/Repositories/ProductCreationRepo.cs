using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<ItemViewModel> RetrieveItemByProduct(ItemViewModel item)
        {
            int productId = item.ProductId;

            var records = from items in db.Items
                          join prod in db.Products on items.ProductId equals prod.ProductId
                          where item.ProductId == prod.ProductId
                          select items;

            return records;
        }

        public IEnumerable<ModelViewModel> RetrieveModelByItemByBrand(ModelViewModel item)
        {
            var records = from models in db.Models
                          join brands in db.Brands on models.BrandId equals brands.BrandId
                          where models.BrandId == item.BrandId
                          && models.ItemId == item.ItemId
                          select models;

            return records;
        }

        public IEnumerable<BrandViewModel> RetrieveBrand()
        {
            var records = from brands in db.Brands
                          select brands;

            return records;
        }

        public IEnumerable<ModelSpecCreationViewModel> RetrieveSpecification(ModelSpecViewModel item)
        {
            var getItemDesc = (from itemObject in db.Items
                               join model in db.Models on itemObject.ItemId equals model.ItemId
                               select itemObject.Description).FirstOrDefault();

            if (getItemDesc != null)
            {
                var getSpec = from specs in db.Specifications
                              join tags in db.Tags on specs.TagId equals tags.TagId
                              where tags.Description == getItemDesc
                              select specs;

                var getModelSpec = (from modelSpecs in db.ModelSpecs
                                    join specs in db.Specifications on modelSpecs.SpecificationId equals specs.SpecificationId
                                    join tags in db.Tags on specs.TagId equals tags.TagId
                                    where tags.Description == getItemDesc
                                    select modelSpecs);

                var modelSpecList = new List<ModelSpecCreationViewModel>();
                var modelSpec = new ModelSpecCreationViewModel();
                if (getSpec != null)
                {
                    foreach (var spec in getSpec)
                    {
                        modelSpec.ModelSpecId = item.ModelSpecId;
                        modelSpec.ModelId = item.ModelId;
                        modelSpec.SpecificationId = spec.SpecificationId;
                        modelSpec.Description = spec.Description;
                        modelSpec.Value = "";
                        modelSpecList.Add(modelSpec);
                    }

                    return modelSpecList.ToList();

                    //foreach (var modSpec in getModelSpec)
                    //{
                    //    var modelSpec = new ModelSpecCreationViewModel()
                    //    {
                    //        ModelId = modSpec.ModelId,
                    //        SpecificationId = spec.SpecificationId,
                    //        Description = (from specs in db.Specifications
                    //                       where specs.SpecificationId == spec.SpecificationId
                    //                       select specs.Description).FirstOrDefault(),
                    //        Value = ""
                    //    };
                    //    modelSpecList.Add(modelSpec);
                    //}
                }
                return null;
            }
            return null;
        }
    }
}