using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IAdminOrderRepo
    {
        IEnumerable<AdminRetrieveOrdersViewModel> AdminRetrieveOrders();
        void ExportToExcel(List<AdminRetrieveOrdersViewModel> items);
    }
}