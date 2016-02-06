using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface ITagRepo
    {
        IEnumerable<SpecialCategoryViewModel> Retrieve();
        SpecialCategoryViewModel Get(int id);
        SpecialCategoryViewModel Create(SpecialCategoryViewModel item);
        SpecialCategoryViewModel Update(int id, SpecialCategoryViewModel item);
    }
}