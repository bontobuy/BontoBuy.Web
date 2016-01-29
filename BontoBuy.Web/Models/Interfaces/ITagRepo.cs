using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface ITagRepo
    {
        IEnumerable<TagViewModel> Retrieve();
        TagViewModel Get(int id);
        TagViewModel Create(TagViewModel item);
        TagViewModel Update(int id, TagViewModel item);
    }
}