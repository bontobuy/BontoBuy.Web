using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BontoBuy.Web.Models
{
    public interface IAdminRoleRepo
    {
        IEnumerable<IdentityRole> RetrieveRoles();
        IdentityRole CreateRole(string roleName);
        IdentityRole UpdateRole(IdentityRole role);
        IdentityRole GetItemForUpdate(string roleName);
        bool DeleteRole(string roleName);
        IEnumerable<AdminRetrieveUsersViewModel> RetrieveUsers();
        IEnumerable<AdminRetrieveUsersViewModel> RetrieveUsers(string filter);
        IEnumerable<AdminRetrieveSuppliersViewModel> RetrieveSuppliers();
        IEnumerable<AdminRetrieveSuppliersViewModel> RetrieveSuppliers(string filter);
        IEnumerable<AdminRetrieveCustomersViewModel> RetrieveCustomers();
        IEnumerable<AdminRetrieveCustomersViewModel> RetrieveCustomers(string filter);
        AdminUpdateSupplierRoleViewModel AdminGetSupplierAccountForUpdate(string id);
        AdminUpdateCustomerRoleViewModel AdminGetCustomerAccountForUpdate(string id);
        AdminUpdateUserRoleViewModel AdminGetUserAccountForUpdate(string id);
        ApplicationUser AdminUpdateSupplierStatus(AdminUpdateSupplierRoleViewModel item, string status);
        ApplicationUser AdminUpdateUserStatus(AdminUpdateUserRoleViewModel item, string status);
        ApplicationUser AdminUpdateCustomerStatus(AdminUpdateCustomerRoleViewModel item, string status);
        IEnumerable<StatusViewModel> AdminRetrieveUserStatus();
        string AdminGetStatus(int id);
    }
}