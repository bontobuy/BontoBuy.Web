using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface ICustomerWishlistRepo
    {
        IEnumerable<WishlistViewModel> Retrieve(string id);
        IEnumerable<CustomerRetrieveWishlistViewModel> RetrieveModels(int id);

        WishlistModelViewModel AddToWishlist(int id);
        WishlistModelViewModel RemoveFromWishlist(int id);
    }
}