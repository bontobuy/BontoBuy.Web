using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IProductRepo
    {
        IEnumerable<ProductViewModel> Retrieve();
        ProductViewModel Get(int id);
        ProductViewModel Create(ProductViewModel item);
        ProductViewModel Update(int id, ProductViewModel item);
    }
}