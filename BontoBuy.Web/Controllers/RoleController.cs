using BontoBuy.Web.HelperMethods;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class RoleController : NotificationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public enum ManageMessageId
        {
            AddRoleSuccess,
            UpdateRoleSuccess,
            ArchiveRoleSuccess,
            RestoreRoleSuccess,
            AddCustomerSuccess,
            UpdateCustomerSuccess,
            ArchiveCustomerSuccess,
            AddSupplierSuccess,
            UpdateSupplierSuccess,
            ArchiveSupplierSuccess,
            Error
        }

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

        public ActionResult RetrieveUsers(ManageMessageId? message)
        {
            if (User.IsInRole("Admin"))
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

                ViewBag.StatusMessage =
                    message == ManageMessageId.AddCustomerSuccess ? "You have successfully added a new Customer."
          : message == ManageMessageId.ArchiveCustomerSuccess ? "You have just desactivated a Customer account."
          : message == ManageMessageId.UpdateCustomerSuccess ? "You have successfully activated a Customer account."
          : message == ManageMessageId.Error ? "An error has occurred."
          : message == ManageMessageId.AddSupplierSuccess ? "You have successfully added a new Supplier."
           : message == ManageMessageId.ArchiveSupplierSuccess ? "You have just desactivated a Supplier account."
           : message == ManageMessageId.UpdateSupplierSuccess ? "You have successfully activated a Supplier account."
           : message == ManageMessageId.Error ? "An error has occurred."
           : "";

                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View(userAccountList);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RetrieveSuppliers(string searchString, ManageMessageId? message)
        {
            if (User.IsInRole("Admin"))
            {
                var userAccountList = new List<UserRoleViewModel>();
                if (!String.IsNullOrEmpty(searchString))
                {
                    var records = db.Suppliers.Where(x => x.Email.Contains(searchString)).ToList();

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
                }
                if (String.IsNullOrEmpty(searchString))
                {
                    var records = db.Suppliers.ToList();

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
                }

                ViewBag.StatusMessage =
           message == ManageMessageId.AddSupplierSuccess ? "You have successfully added a new Supplier."
           : message == ManageMessageId.ArchiveSupplierSuccess ? "You have just desactivated a Supplier account."
           : message == ManageMessageId.UpdateSupplierSuccess ? "You have successfully activated a Supplier account."
           : message == ManageMessageId.Error ? "An error has occurred."
           : "";

                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View(userAccountList);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RetrieveCustomers(string searchString, ManageMessageId? message)
        {
            if (User.IsInRole("Admin"))
            {
                var userAccountList = new List<UserRoleViewModel>();

                if (!String.IsNullOrEmpty(searchString))
                {
                    var records = db.Customers.Where(x => x.Email.Contains(searchString)).ToList();
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
                }
                if (String.IsNullOrEmpty(searchString))
                {
                    var records = db.Customers.ToList();
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
                }

                ViewBag.StatusMessage =
          message == ManageMessageId.AddCustomerSuccess ? "You have successfully added a new Customer."
          : message == ManageMessageId.ArchiveCustomerSuccess ? "You have just desactivated a Customer account."
          : message == ManageMessageId.UpdateCustomerSuccess ? "You have successfully activated a Customer account."
          : message == ManageMessageId.Error ? "An error has occurred."
          : "";

                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View(userAccountList);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ManageRoles()
        {
            GetNewSupplierActivation();
            GetNewModelsActivation();
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

                //ViewBag.UserRoles = userRole.UserRoles
                //    .Select(r => new SelectListItem { Value = r.Name, Text = r.Name });
                ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Name");

                GetNewSupplierActivation();
                GetNewModelsActivation();
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

                //ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Name", statusId);
                //string status = (from s in db.Statuses
                //                 where s.StatusId == statusId
                //                 select s.Name).FirstOrDefault();

                var user = (from u in db.Users
                            where u.Id == item.UserId
                            select u).FirstOrDefault();

                if (user.Status == "Active")
                {
                    user.Status = "Inactive";
                }
                else if (user.Status == "Inactive")
                {
                    user.Status = "Active";
                }
                else if (user.Status == "Pending")
                {
                    user.Status = "Active";
                }

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

                //  await UserManager.AddToRoleAsync(item.UserId, roleName);
                var roles = await UserManager.GetRolesAsync(user.Id);
                var userRole = new List<string>(roles);

                var matchingValueSupplier = userRole.FirstOrDefault(stringToCompare => stringToCompare.Contains("Supplier"));
                if (matchingValueSupplier == "Supplier")
                    return RedirectToAction("RetrieveSuppliers");

                var matchingValueCustomer = userRole.FirstOrDefault(stringToCompare => stringToCompare.Contains("Customer"));
                if (matchingValueCustomer == "Customer")
                    return RedirectToAction("RetrieveCustomers");

                GetNewSupplierActivation();
                GetNewModelsActivation();
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
                GetNewSupplierActivation();
                GetNewModelsActivation();
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
                        return RedirectToAction("Retrieve", new { message = ManageMessageId.AddRoleSuccess });
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
                GetNewSupplierActivation();
                GetNewModelsActivation();
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

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
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

            GetNewSupplierActivation();
            GetNewModelsActivation();
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