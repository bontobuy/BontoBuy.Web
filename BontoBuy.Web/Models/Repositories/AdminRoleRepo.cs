using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BontoBuy.Web.HelperMethods;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BontoBuy.Web.Models
{
    public class AdminRoleRepo : IAdminRoleRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Helper helper = new Helper();

        public IEnumerable<IdentityRole> RetrieveRoles()
        {
            var records = db.Roles.Where(x => x.Name != "Admin").ToList();
            if (records == null)
                return null;

            return records;
        }
        public IdentityRole CreateRole(string roleName)
        {
            if (String.IsNullOrWhiteSpace(roleName))
                return null;

            string titleCaseRoleName = helper.ConvertToTitleCase(roleName.ToLowerInvariant());

            var existingRecord = CheckDuplicates(titleCaseRoleName);

            if (existingRecord != null)
                return existingRecord;

            var newItem = new IdentityRole()
            {
                Name = titleCaseRoleName
            };
            db.SaveChanges();

            return newItem;
        }
        public IdentityRole UpdateRole(IdentityRole role)
        {
            if (role == null)
                return null;

            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();

            return role;
        }
        public IdentityRole GetItemForUpdate(string roleName)
        {
            var record = db.Roles.Where(x => x.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (record == null)
                return null;

            return record;
        }
        public bool DeleteRole(string roleName)
        {
            var record = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (record == null)
                return false;

            db.Roles.Remove(record);
            db.SaveChanges();

            return true;
        }
        public IdentityRole CheckDuplicates(string roleName)
        {
            if (String.IsNullOrWhiteSpace(roleName))
                return null;

            var record = db.Roles.Where(x => x.Name == roleName).FirstOrDefault();
            if (record != null)
                return record;

            return null;
        }

        public IEnumerable<AdminRetrieveUsersViewModel> RetrieveUsers()
        {
            var records = db.Users.ToList();
            if (records == null)
                return null;

            var itemList = AssignUserList(records);
            if (itemList == null)
                return null;

            return itemList;
        }
        public IEnumerable<AdminRetrieveUsersViewModel> RetrieveUsers(string filter)
        {
            if (String.IsNullOrWhiteSpace(filter))
                return null;

            var records = db.Users.Where(x => x.Email.Contains(filter)).ToList();
            if (records == null)
                return null;

            var itemList = AssignUserList(records);
            if (itemList == null)
                return null;

            return itemList;
        }
        public IEnumerable<AdminRetrieveSuppliersViewModel> RetrieveSuppliers()
        {
            var records = db.Suppliers.ToList();
            if (records == null)
                return null;

            var itemList = AssignSupplierList(records);
            if (itemList == null)
                return null;

            return itemList;
        }
        public IEnumerable<AdminRetrieveSuppliersViewModel> RetrieveSuppliers(string filter)
        {
            if (String.IsNullOrWhiteSpace(filter))
                return null;

            var records = db.Suppliers.Where(x => x.Email.Contains(filter)).ToList();
            if (records == null)
                return null;

            var itemList = AssignSupplierList(records);
            if (itemList == null)
                return null;

            return itemList;
        }
        public IEnumerable<AdminRetrieveCustomersViewModel> RetrieveCustomers()
        {
            var records = db.Customers.ToList();
            if (records == null)
                return null;

            var itemList = AssignCustomerList(records);
            if (itemList == null)
                return null;

            return itemList;
        }
        public IEnumerable<AdminRetrieveCustomersViewModel> RetrieveCustomers(string filter)
        {
            if (String.IsNullOrWhiteSpace(filter))
                return null;

            var records = db.Customers.Where(x => x.Email.Contains(filter)).ToList();
            if (records == null)
                return null;

            var itemList = AssignCustomerList(records);
            if (itemList == null)
                return null;

            return itemList;
        }
        public AdminUpdateSupplierRoleViewModel AdminGetSupplierAccountForUpdate(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            var record = db.Suppliers.Where(x => x.Id == id).FirstOrDefault();
            if (record == null)
                return null;

            var itemToUpdate = AssignSupplierDetailsForUpdate(record);

            return itemToUpdate;
        }
        public AdminUpdateCustomerRoleViewModel AdminGetCustomerAccountForUpdate(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            var record = db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (record == null)
                return null;
            var itemToUpdate = AssignCustomerDetailsForUpdate(record);

            return itemToUpdate;
        }

        public AdminUpdateUserRoleViewModel AdminGetUserAccountForUpdate(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            var record = db.Users.Where(x => x.Id == id).FirstOrDefault();
            if (record == null)
                return null;
            var itemToUpdate = AssignUserDetailsForUpdate(record);

            return itemToUpdate;
        }

        private AdminUpdateUserRoleViewModel AssignUserDetailsForUpdate(ApplicationUser record)
        {
            if (record == null || String.IsNullOrWhiteSpace(record.Id))
                return null;

            var itemToUpdate = new AdminUpdateUserRoleViewModel()
            {
                UserId = record.Id,
                Email = record.Email,
                Status = record.Status,
                UserRoles = db.Roles.OrderBy(x => x.Name).ToList()
            };

            if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.UserId))
                return null;

            return itemToUpdate;
        }
        public ApplicationUser AdminUpdateSupplierStatus(AdminUpdateSupplierRoleViewModel item, string status)
        {
            var record = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
            if (record == null || String.IsNullOrWhiteSpace(record.Id))
                return null;

            var itemToUpdate = AssignUserStatusForUpdate(record, status);
            if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.Id))
                return null;

            return itemToUpdate;
        }

        public ApplicationUser AdminUpdateUserStatus(AdminUpdateUserRoleViewModel item, string status)
        {
            var record = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
            if (record == null || String.IsNullOrWhiteSpace(record.Id))
                return null;

            var itemToUpdate = AssignUserStatusForUpdate(record, status);
            if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.Id))
                return null;

            return itemToUpdate;
        }
        public ApplicationUser AdminUpdateCustomerStatus(AdminUpdateCustomerRoleViewModel item, string status)
        {
            var record = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
            if (record == null || String.IsNullOrWhiteSpace(record.Id))
                return null;

            var itemToUpdate = AssignUserStatusForUpdate(record, status);
            if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.Id))
                return null;

            return itemToUpdate;
        }

        public IEnumerable<StatusViewModel> AdminRetrieveUserStatus()
        {
            var records = db.Statuses.ToList();
            if (records == null)
                return null;

            return records;
        }

        public string AdminGetStatus(int id)
        {
            if (id < 1)
                return null;

            var record = (from s in db.Statuses
                          where s.StatusId == id
                          select s.Name).FirstOrDefault();
            if (record == null)
                return null;

            return record;
        }

        private AdminUpdateCustomerRoleViewModel AssignCustomerDetailsForUpdate(CustomerViewModel record)
        {
            if (record == null || String.IsNullOrWhiteSpace(record.Id))
                return null;

            var itemToUpdate = new AdminUpdateCustomerRoleViewModel()
            {
                UserId = record.Id,
                Email = record.Email,
                Status = record.Status,
                UserRoles = db.Roles.OrderBy(x => x.Name).ToList()
            };

            if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.UserId))
                return null;

            return itemToUpdate;
        }
        private AdminUpdateSupplierRoleViewModel AssignSupplierDetailsForUpdate(SupplierViewModel record)
        {
            if (record == null || String.IsNullOrWhiteSpace(record.Id))
                return null;

            var itemToUpdate = new AdminUpdateSupplierRoleViewModel()
            {
                UserId = record.Id,
                Email = record.Email,
                Status = record.Status,
                UserRoles = db.Roles.OrderBy(x => x.Name).ToList()
            };

            if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.UserId))
                return null;

            return itemToUpdate;
        }

        private IEnumerable<AdminRetrieveUsersViewModel> AssignUserList(List<ApplicationUser> records)
        {
            if (records == null)
                return null;
            var userList = new List<AdminRetrieveUsersViewModel>();

            foreach (var item in records)
            {
                var userItem = new AdminRetrieveUsersViewModel()
                {
                    UserId = item.Id,
                    Email = item.Email,
                    Status = item.Status
                };
                userList.Add(userItem);
            }
            return userList;
        }

        private IEnumerable<AdminRetrieveSuppliersViewModel> AssignSupplierList(List<SupplierViewModel> records)
        {
            if (records == null)
                return null;

            var userList = new List<AdminRetrieveSuppliersViewModel>();

            foreach (var item in records)
            {
                var userItem = new AdminRetrieveSuppliersViewModel()
                {
                    UserId = item.Id,
                    Email = item.Email,
                    Status = item.Status
                };
                userList.Add(userItem);
            }
            return userList;
        }

        private IEnumerable<AdminRetrieveCustomersViewModel> AssignCustomerList(List<CustomerViewModel> records)
        {
            if (records == null)
                return null;

            var userList = new List<AdminRetrieveCustomersViewModel>();

            foreach (var item in records)
            {
                var userItem = new AdminRetrieveCustomersViewModel()
                {
                    UserId = item.Id,
                    Email = item.Email,
                    Status = item.Status
                };
                userList.Add(userItem);
            }
            return userList;
        }

        private ApplicationUser AssignUserStatusForUpdate(ApplicationUser record, string status)
        {
            if (record == null || String.IsNullOrWhiteSpace(record.Id) || String.IsNullOrWhiteSpace(status))
                return null;

            record.Status = status;
            db.SaveChanges();

            return record;
        }
    }
}