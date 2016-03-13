using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BontoBuy.Web.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly IAdminRoleRepo _repo;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AdminRoleController(IAdminRoleRepo repository, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _repo = repository;
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

        public ActionResult RetrieveRoles()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveRoles();
                    return View(records);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult CreateRole()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    return View();
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(FormCollection fc)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    string roleName = fc["RoleName"];
                    if (String.IsNullOrWhiteSpace(roleName))
                        return RedirectToAction("Home", "Error404");

                    var newItem = _repo.CreateRole(roleName);
                    if (newItem == null)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("RetrieveRoles");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult UpdateRole(string roleName)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (String.IsNullOrWhiteSpace(roleName))
                        return RedirectToAction("Home", "Error404");

                    var itemToUpdate = _repo.GetItemForUpdate(roleName);
                    if (itemToUpdate == null)
                        return RedirectToAction("Home", "Error404");

                    return View(itemToUpdate);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRole(IdentityRole item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (item == null)
                        return RedirectToAction("Home", "Error404");

                    var itemToUpdate = _repo.UpdateRole(item);
                    if (itemToUpdate == null)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("RetrieveRoles");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult DeleteRole(string roleName)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (String.IsNullOrWhiteSpace(roleName))
                        return RedirectToAction("Home", "Error404");

                    var itemToDelete = _repo.DeleteRole(roleName);
                    if (itemToDelete == false)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("RetrieveRoles");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveUsers()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveUsers();
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveUsers(string searchString)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveUsers(searchString);
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveSuppliers()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveSuppliers();
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveSuppliers(string searchString)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveSuppliers(searchString);
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveCustomers()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveCustomers();
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        public ActionResult RetrieveCustomers(string searchString)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveCustomers(searchString);
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult UpdateUser(string id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (String.IsNullOrWhiteSpace(id))
                        return RedirectToAction("Home", "Error404");

                    var itemToUpdate = _repo.AdminGetUserAccountForUpdate(id);
                    if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.UserId))
                        return RedirectToAction("Home", "Error404");

                    return View(itemToUpdate);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(AdminUpdateUserRoleViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item == null)
                            return RedirectToAction("Home", "Error404");

                        var statusList = _repo.AdminRetrieveUserStatus();
                        if (statusList == null)
                            return RedirectToAction("Home", "Error404");

                        int statusId = 1;
                        ViewBag.StatusId = new SelectList(statusList, "StatusId", "Name", statusId);

                        var itemStatus = _repo.AdminGetStatus(statusId);
                        if (String.IsNullOrWhiteSpace(itemStatus))
                            return RedirectToAction("Home", "Error404");

                        var itemToUpdate = _repo.AdminUpdateUserStatus(item, itemStatus);
                        if (itemToUpdate == null)
                            return RedirectToAction("Home", "Error404");

                        return RedirectToAction("RetrieveUsers");
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult UpdateSupplier(string id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (String.IsNullOrWhiteSpace(id))
                        return RedirectToAction("Home", "Error404");

                    var itemToUpdate = _repo.AdminGetSupplierAccountForUpdate(id);
                    if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.UserId))
                        return RedirectToAction("Home", "Error404");

                    return View(itemToUpdate);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSupplier(AdminUpdateSupplierRoleViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item == null)
                            return RedirectToAction("Home", "Error404");

                        var statusList = _repo.AdminRetrieveUserStatus();
                        if (statusList == null)
                            return RedirectToAction("Home", "Error404");

                        int statusId = 1;
                        ViewBag.StatusId = new SelectList(statusList, "StatusId", "Name", statusId);

                        var itemStatus = _repo.AdminGetStatus(statusId);
                        if (String.IsNullOrWhiteSpace(itemStatus))
                            return RedirectToAction("Home", "Error404");

                        var itemToUpdate = _repo.AdminUpdateSupplierStatus(item, itemStatus);
                        if (itemToUpdate == null)
                            return RedirectToAction("Home", "Error404");

                        new Task(() => { SendNotification(itemToUpdate); }).Start();

                        return RedirectToAction("RetrieveUsers");
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult UpdateCustomer(string id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (String.IsNullOrWhiteSpace(id))
                        return RedirectToAction("Home", "Error404");

                    var itemToUpdate = _repo.AdminGetCustomerAccountForUpdate(id);
                    if (itemToUpdate == null || String.IsNullOrWhiteSpace(itemToUpdate.UserId))
                        return RedirectToAction("Home", "Error404");

                    return View(itemToUpdate);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(AdminUpdateCustomerRoleViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item == null)
                            return RedirectToAction("Home", "Error404");

                        var statusList = _repo.AdminRetrieveUserStatus();
                        if (statusList == null)
                            return RedirectToAction("Home", "Error404");

                        int statusId = 1;
                        ViewBag.StatusId = new SelectList(statusList, "StatusId", "Name", statusId);

                        var itemStatus = _repo.AdminGetStatus(statusId);
                        if (String.IsNullOrWhiteSpace(itemStatus))
                            return RedirectToAction("Home", "Error404");

                        var itemToUpdate = _repo.AdminUpdateCustomerStatus(item, itemStatus);
                        if (itemToUpdate == null)
                            return RedirectToAction("Home", "Error404");

                        return RedirectToAction("RetrieveUsers");
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        private static async void SendNotification(ApplicationUser item)
        {
            if (item.Status == "Active" && item.ActivationCode != null)
            {
                var body = "<p>Dear Valued Customer,</p><p>This is the activation code that has been sent to you in order to validate your registration on BontoBuy</p><p>Your activation code: {0}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(item.Email));
                message.From = new MailAddress("bontobuy@gmail.com");
                message.Subject = "Register on BontoBuy";
                message.Body = string.Format(body, item.ActivationCode);
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
        }
    }
}