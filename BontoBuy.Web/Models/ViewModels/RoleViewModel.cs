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

    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}