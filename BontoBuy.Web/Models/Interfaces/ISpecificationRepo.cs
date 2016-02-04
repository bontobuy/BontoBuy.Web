using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public interface ISpecificationRepo
    {
        IEnumerable<SpecificationViewModel> Retrieve();
        SpecificationViewModel Get(int id);
        SpecificationViewModel Create(SpecificationViewModel item);
        SpecificationViewModel Update(int id, SpecificationViewModel item);
        void Archive(int id);
        IEnumerable<SpecificationViewModel> RetrieveArchives();
        void RevertArchive(int id);
    }
}