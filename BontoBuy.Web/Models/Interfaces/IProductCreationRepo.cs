using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public interface IProductCreationRepo
    {
        IEnumerable<ProductCreationViewModel> Retrieve();
        ProductCreationViewModel Get(int id);
        ProductCreationViewModel Create(ProductCreationViewModel item);
        ProductCreationViewModel Update(int id, ProductCreationViewModel item);

        IEnumerable<ProductViewModel> RetrieveProductByCategory(ProductViewModel item);

        //ProductCreationViewModel AssignProductValue(ProductViewModel item);
        IEnumerable<ItemViewModel> RetrieveItemByProduct(ItemViewModel item);
        IEnumerable<BrandViewModel> RetrieveBrand();
        IEnumerable<ModelViewModel> RetrieveModelByItemByBrand(ModelViewModel item);
        IEnumerable<ModelSpecCreationViewModel> RetrieveSpecification(ModelSpecViewModel item);
    }
}