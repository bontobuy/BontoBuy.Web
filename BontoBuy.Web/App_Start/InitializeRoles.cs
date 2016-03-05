using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BontoBuy.Web.HelperMethods;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BontoBuy.Web.App_Start
{
    public class InitializeRoles
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public InitializeRoles()
        {
        }
        public InitializeRoles(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }

            //return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public void InitializeAllRoles()
        {
            var roleList = new List<string>()
            {
                "Admin","Customer","Supplier","Sales"
            };
            if (roleList != null)
            {
                var helper = new Helper();
                foreach (var item in roleList)
                {
                    string titleCaseRole = helper.ConvertToTitleCase(item.ToLowerInvariant());

                    var searchResult = (from role in db.Roles
                                        where role.Name == titleCaseRole
                                        select role).FirstOrDefault();

                    if (searchResult == null)
                    {
                        db.Roles.Add(new IdentityRole()
                        {
                            Name = titleCaseRole
                        });
                        db.SaveChanges();
                    }
                }
            }
        }

        public async void InitializeAdmin()
        {
            if (!db.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                string password = "P@ssw0rd";
                var admin = new AdminViewModel()
                {
                    Email = "admin@admin.com",
                    FirstName = "admin",
                    LastName = "admin",
                    DtCreated = DateTime.UtcNow,
                    DtUpdated = DateTime.UtcNow,
                    Status = "Active",
                    Name = "admin",
                    UserName = "admin"
                };

                var result = await UserManager.CreateAsync(admin, password);

                if (!result.Succeeded)
                {
                    throw new System.ArgumentOutOfRangeException("Operation did not succeed");
                }
                if (result.Succeeded)
                {
                    ApplicationUser currentUser = db.Users.Where(u => u.Email.Equals(admin.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (admin != null) UserManager.AddToRole(admin.Id, "Admin");
                }
            }
        }
    }
}