using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BontoBuy.Web.Models
{
    public class RoleViewModel
    {
    }

    public class AdminRetrieveUsersViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

    public class AdminRetrieveCustomersViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

    public class AdminRetrieveSuppliersViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

    public class AdminUpdateSupplierRoleViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

    public class AdminUpdateCustomerRoleViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

    public class AdminUpdateUserRoleViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}