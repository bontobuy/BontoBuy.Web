using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IAdminItemRepo
    {
        IEnumerable<AdminRetrieveItemViewModel> Retrieve();
        AdminGetItemViewModel Get(int id);
        ItemViewModel Create(AdminCreateItemViewModel item);
        ItemViewModel Update(AdminUpdateItemViewModel item);
        AdminUpdateItemViewModel GetUpdateItemViewModel(int id);
        IEnumerable<AdminRetrieveItemViewModel> RetrieveArchives();
        bool Archive(int id);
        bool RevertArchive(int id);

        List<ProductViewModel> AdminRetrieveProducts();
    }
}