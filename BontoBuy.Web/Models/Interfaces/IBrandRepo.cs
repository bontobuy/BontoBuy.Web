using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IBrandRepo
    {
        IEnumerable<BrandViewModel> Retrieve();
        BrandViewModel Get(int id);
        BrandViewModel Create(BrandViewModel item);
        BrandViewModel Update(int id, BrandViewModel item);
    }
}