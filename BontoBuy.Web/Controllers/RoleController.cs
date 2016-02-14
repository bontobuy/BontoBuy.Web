using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.HelperMethods;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BontoBuy.Web.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }

            //return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public ActionResult RetrieveUsers()
        {
            var records = db.Users.ToList();

            var userAccountList = new List<UserRoleViewModel>();
            foreach (var item in records)
            {
                var userAccount = new UserRoleViewModel()
                {
                    UserId = item.Id,
                    Email = item.Email,
                    Status = item.Status
                };
                userAccountList.Add(userAccount);
            }
            return View(userAccountList);
        }

        public async Task<ActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return RedirectToAction("RetrieveUsers");
            }

            string userId = id;

            var user = (from u in db.Users
                        where u.Id == userId
                        select u).FirstOrDefault();

            var userRole = new UserRoleViewModel();

            userRole.UserId = userId;
            userRole.Email = user.Email;
            userRole.Status = user.Status;
            userRole.UserRoles = db.Roles.OrderBy(x => x.Name).ToList();

            var roles = await UserManager.GetRolesAsync(user.Id);
            userRole.CurrentRoles = new List<string>(roles);
            userRole.CurrentRoles.Sort();

            ViewBag.UserRoles = userRole.UserRoles
                .Select(r => new SelectListItem { Value = r.Name, Text = r.Name });
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Name");

            return View(userRole);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UserRoleViewModel item, string roleName)
        {
            int statusId = 1;
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Name", statusId);

            string status = (from s in db.Statuses
                             where s.StatusId == statusId
                             select s.Name).FirstOrDefault();

            var user = (from u in db.Users
                        where u.Id == item.UserId
                        select u).FirstOrDefault();

            user.Status = status;
            db.SaveChanges();
            await UserManager.AddToRoleAsync(item.UserId, roleName);

            return RedirectToAction("RetrieveUsers");
        }

        // GET: Role
        public ActionResult Retrieve()
        {
            var records = db.Roles.ToList();

            return View(records);
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string roleName = collection["RoleName"];

                if (!String.IsNullOrEmpty(roleName))
                {
                    var helper = new Helper();

                    string titleCaseRoleName = helper.ConvertToTitleCase(roleName.ToLowerInvariant());

                    var search = (from role in db.Roles
                                  where role.Name == titleCaseRoleName
                                  select role).FirstOrDefault();

                    if (search == null)
                    {
                        db.Roles.Add(new IdentityRole()
                        {
                            Name = titleCaseRoleName
                        });

                        db.SaveChanges();
                        return RedirectToAction("Retrieve");
                    }

                    return Content("Success");
                }
                return Content("failed");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(string roleName)
        {
            var role = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                // TODO: Add update logic here
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Retrieve");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Role/Delete/5
        //public ActionResult Delete()
        //{
        //    return View();
        //}

        // POST: Role/Delete/5
        //[HttpPost]
        public ActionResult Delete(string roleName)
        {
            try
            {
                // TODO: Add delete logic here
                var role = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                db.Roles.Remove(role);
                db.SaveChanges();

                return RedirectToAction("Retrieve");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ManageUserRoles()
        {
            var roleList = db.Roles.OrderBy(r => r.Name).ToList().Select(r =>
                new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();

            ViewBag.Roles = roleList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string email, string roleName)
        {
            ApplicationUser user = db.Users.Where(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var account = new AccountController();
            account.UserManager.AddToRole(user.Id, roleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        public ActionResult GetRoles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRoles(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var roles = await UserManager.GetRolesAsync(user.Id);

                ViewBag.RolesForThisUser = roles;

                // prepopulat roles for the view dropdown
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
                ViewBag.Roles = list;

                return View("ManageUserRoles");
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string email, string roleName)
        {
            var account = new AccountController();
            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, roleName))
            {
                account.UserManager.RemoveFromRole(user.Id, roleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }

            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }
    }
}