using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IItemRepo
    {
        IEnumerable<ItemViewModel> Retrieve();
        ItemViewModel Get(int id);
        ItemViewModel Create(ItemViewModel item);
        ItemViewModel Update(int id, ItemViewModel item);
        void Remove(int id);
    }
}