using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface ICategoryRepo
    {
        IEnumerable<CategoryViewModel> Retrieve();
        CategoryViewModel Get(int id);
        CategoryViewModel Create(CategoryViewModel item);
        CategoryViewModel Update(int id, CategoryViewModel item);
    }
}