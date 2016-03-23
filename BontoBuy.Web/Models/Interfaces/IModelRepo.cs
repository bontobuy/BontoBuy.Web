using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IModelRepo
    {
        IEnumerable<ModelViewModel> Retrieve();
        ModelAdminViewModel Get(int id);
        ModelViewModel Create(ModelViewModel item);
        ModelViewModel Update(int id, ModelViewModel item);
        void ExportToExcel(List<ModelAdminRetrieveViewModel> records);
        void Archive(int id);
        IEnumerable<ModelViewModel> RetrieveArchives();
        void RevertArchive(int id);
    }
}