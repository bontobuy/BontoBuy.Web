using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface ISupplierOrderRepo
    {
        IEnumerable<OrderViewModel> Retrieve(string id);
        OrderViewModel Get(int id);
        OrderViewModel UpdateOrderStatus(OrderViewModel item);
    }
}