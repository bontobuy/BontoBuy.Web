using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            if (User.IsInRole("Admin"))
            {
                var records = db.Suppliers.ToList();

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
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RetrieveSuppliers()
        {
            if (User.IsInRole("Admin"))
            {
                var records = db.Suppliers.ToList();

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
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RetrieveCustomers()
        {
            if (User.IsInRole("Admin"))
            {
                var records = db.Customers.ToList();

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
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ManageRoles()
        {
            return View();
        }

        public async Task<ActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return RedirectToAction("RetrieveUsers");
            }
            if (User.IsInRole("Admin"))
            {
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
            return RedirectToAction("RetrieveUsers");
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UserRoleViewModel item, string roleName)
        {
            if (User.IsInRole("Admin"))
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

                if (user.Status == "Active" && user.ActivationCode != null)
                {
                    var body = "<p>Dear Valued Customer,</p><p>This is the activation code that has been sent to you in order to validate your registration on BontoBuy</p><p>Your activation code: {0}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(user.Email));
                    message.From = new MailAddress("bontobuy@gmail.com");
                    message.Subject = "Register on BontoBuy";
                    message.Body = string.Format(body, user.ActivationCode);
                    message.IsBodyHtml = true;

                    var smtp = new SmtpClient();

                    var credential = new NetworkCredential()
                    {
                        UserName = "bontobuy@gmail.com",
                        Password = "b0nt0@dmin"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }

                await UserManager.AddToRoleAsync(item.UserId, roleName);
                return RedirectToAction("RetrieveUsers");
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Role
        public ActionResult Retrieve()
        {
            if (User.IsInRole("Admin"))
            {
                var records = db.Roles.ToList();
                return View(records);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
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
            if (User.IsInRole("Admin"))
            {
                var role = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                return View(role);
            }

            return RedirectToAction("Login", "Account");
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    // TODO: Add update logic here
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Retrieve");
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
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
                if (User.IsInRole("Admin"))
                {
                    // TODO: Add delete logic here
                    var role = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    db.Roles.Remove(role);
                    db.SaveChanges();

                    return RedirectToAction("Retrieve");
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return RedirectToAction("Login", "Account");
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