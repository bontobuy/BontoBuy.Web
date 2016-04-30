using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IAdminCommissionRepo
    {
        IEnumerable<CommissionViewModel> Retrieve();
        CommissionViewModel Get(int id);
        CommissionViewModel Create(CommissionViewModel item);
        CommissionViewModel Update(CommissionViewModel item);

        IEnumerable<OrderViewModel> RetrieveDeliveredOrders();
        IEnumerable<OrderViewModel> RetrievePaidOrders();
        OrderViewModel UpdateCommissionOwnedFromOrders(int id);
        void ExportToExcel(List<CommissionOrderViewModel> items);
    }
}