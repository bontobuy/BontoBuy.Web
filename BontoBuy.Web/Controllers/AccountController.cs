using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BontoBuy.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
            set
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
            set
            {
                _userManager = value;
            }

            //return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public static string CodeGenerator()
        {
            int length = 8;
            const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            var randomNumber = new Random();
            return new string(Enumerable.Repeat(Chars, length)
            .Select(s => s[randomNumber.Next(s.Length)]).ToArray());
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
               message == ManageMessageId.ActivationFailure ? "You have entered a wrong Activation Code."
               : message == ManageMessageId.Error ? "An error has occurred."
               : "";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = db.Users.Where(x => x.Email == model.Email).Select(x => x.Id).FirstOrDefault();
            if (String.IsNullOrWhiteSpace(userId))
                return View(model);
            var userProfile = (from u in db.Users
                               where u.Id == userId
                               select u.ActivationCode).FirstOrDefault();

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            if (result.ToString() == "Success")
            {
                Session["AccountInfo"] = userId;
                var RolesForUser = await UserManager.GetRolesAsync(userId);
                string password = db.Users.Where(x => x.Email == model.Email)
                                     .Select(x => x.PasswordHash)
                                     .Single();
                bool passwordMatches = Crypto.VerifyHashedPassword(password, model.Password);

                string requestedUrl = Session["InitialRequest"] as string;

                // string requestedUrl = returnUrl;
                if (userId != null && passwordMatches == true)
                {
                    switch (RolesForUser[0].ToString())
                    {
                        case "Supplier":
                            if (userProfile != null)
                            {
                                return RedirectToAction("ActivateAccount");
                            }
                            if (String.IsNullOrWhiteSpace(returnUrl))
                            {
                                return RedirectToAction("Index", "Supplier");
                            }
                            return Redirect(returnUrl);

                        case "Admin":
                            return RedirectToAction("Index", "Admin");

                        case "Customer":
                            if (userProfile != null)
                            {
                                return RedirectToAction("ActivateAccount", "Account");
                            }

                            if (String.IsNullOrWhiteSpace(returnUrl))
                            {
                                return RedirectToAction("Index", "Customer");
                            }

                            //if (pendingUser.ActivationCode != null)
                            //{
                            //    return RedirectToAction("ActivateAccount", "Account");
                            //}
                            return Redirect(returnUrl);
                    }
                    return RedirectToAction("ActivateAccount", "Account");
                }
            }

            switch (result)
            {
                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        public ActionResult ActivateAccount()
        {
            var account = new AccountViewModel();
            return View(account);
        }

        [HttpPost]
        public ActionResult ActivateAccount(AccountViewModel item)
        {
            //Assign the current userId of the current user to variable userId
            var userId = Session["AccountInfo"] as string;

            //Remove the session variable
            Session.Remove("AccountInfo");

            //Check if variable userId is null
            if (String.IsNullOrEmpty(userId))

                //Redirect to Login form if it is null
                return RedirectToAction("Login", "Account");

            //Search and assing user the user which corresponds to the userId to variable requestedUser
            var requestedUser = db.Users.Where(x => x.Id == userId).FirstOrDefault();

            //Verify if the activation code entered and that of the account matches
            if (requestedUser.ActivationCode == item.ActivationCode)
            {
                //Search and assing user the user which corresponds to the userId to variable currentUser
                var currentUser = db.Users.Where(x => x.Id == userId).FirstOrDefault();

                //If currentUser is null. Display error page
                if (String.IsNullOrEmpty(currentUser.Id))
                    return RedirectToAction("Error404", "Home");

                //If the current user is not null assing null update the ActivationCode column of the current user
                currentUser.ActivationCode = null;

                //Save the changes into the database
                db.SaveChanges();

                //Redirect to the homepage
                return RedirectToAction("Index", "Home", new { message = ManageMessageId.ActivationSuccess });
            }
            return RedirectToAction("Login", "Account", new { message = ManageMessageId.ActivationFailure });
        }

        // GET: /Account/LoginAdmin
        [AllowAnonymous]
        public ActionResult LoginAdmin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAdmin(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:

                    return RedirectToAction("Index", "Admin");

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult LoginSupplier(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginSupplier(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Supplier");

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterCustomerViewModel model)
        {
            //Verify if the email address is in use on the system
            var emailValid = (from u in db.Users
                              where u.Email == model.Email
                              select u).Count();

            //If the email is already in use the user receives an error message
            if (emailValid > 0)
            {
                ViewBag.StatusMessage = "Email already exists.";
            }

            //Verify if the model is valid
            //if the model is valid then proceed
            if (ModelState.IsValid)
            {
                //Generate activation code and store it in the variable activationCode
                var activationCode = CodeGenerator();

                //Create a new instance of CustomerViewModel and store it in object customer
                var customer = new CustomerViewModel()
                {
                    UserName = model.Email,
                    Email = model.Email,

                    //Assign value of activationCode to the property ActivationCode of customer
                    ActivationCode = activationCode,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Name = model.FirstName + " " + model.LastName,
                    Status = "Pending",
                    DtCreated = DateTime.UtcNow,
                    PhoneNumber = model.PhoneNumber
                };

                //Async method to create a new customer
                var result = await UserManager.CreateAsync(customer, model.Password);

                //If the method property succeeds
                if (result.Succeeded)
                {
                    //Signin the new customer in the application
                    await SignInManager.SignInAsync(customer, isPersistent: false, rememberBrowser: false);

                    //Create an instance of DeliveryAddressViewModel and store it variable customerAddress
                    var customerAddress = new DeliveryAddressViewModel()
                    {
                        Street = model.Street,
                        City = model.City,
                        UserId = customer.Id,
                        CustomerId = customer.CustomerId,
                        Zipcode = model.ZipCode,
                        Status = "Default"
                    };
                    db.DeliveryAddresses.Add(customerAddress);

                    //Save changes into the database
                    db.SaveChanges();

                    //It the body of the email that will be sent to the user after the registration process
                    var body = "<p>Dear Valued Customer,</p><p>This is the activation code that has been sent to you in order to validate your registration on BontoBuy</p>" +
                        "<p>Your activation code: {0}</p>";

                    var message = new MailMessage();

                    //It contains the recipient of the email
                    message.To.Add(new MailAddress(model.Email));

                    //It contains the email address of BontoBuy
                    message.From = new MailAddress("bontobuy@gmail.com");

                    //Subject of the mail
                    message.Subject = "Register on BontoBuy";

                    //Using formatted string the activation code is then added to the body of the email
                    message.Body = string.Format(body, activationCode);
                    message.IsBodyHtml = true;

                    var smtp = new SmtpClient();

                    //Use credential of BontoBuy email
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

                    ApplicationUser currentUser = db.Users.Where(u => u.Email.Equals(model.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (customer != null) UserManager.AddToRole(customer.Id, "Customer");

                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return RedirectToAction("RegistrationLogin", "Account");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult RegisterSales()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterSales(RegisterSalesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sales = new AdminViewModel()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    DtCreated = DateTime.UtcNow,
                    Name = model.Name
                };
                var result = await UserManager.CreateAsync(sales, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(sales, isPersistent: false, rememberBrowser: false);

                    ApplicationUser user = db.Users.Where(u => u.Email.Equals(model.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (user != null) UserManager.AddToRole(user.Id, "Sales");

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Sales");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/RegisterAdmin
        [AllowAnonymous]
        public ActionResult RegisterAdmin()
        {
            return View();
        }

        //
        // POST: /Account/RegisterAdmin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAdmin(RegisterAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = new AdminViewModel()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    DtCreated = DateTime.UtcNow,
                    Name = model.Name
                };
                var result = await UserManager.CreateAsync(admin, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(admin, isPersistent: false, rememberBrowser: false);

                    ApplicationUser user = db.Users.Where(u => u.Email.Equals(model.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (user != null) UserManager.AddToRole(user.Id, "Admin");

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Admin");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterSupplier()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterSupplier(RegisterSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                var activationCode = CodeGenerator();
                var supplier = new SupplierViewModel()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    DtCreated = DateTime.UtcNow,
                    Name = model.Name,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Website = model.Website,
                    Status = "Pending",
                    PhoneNumber = model.PhoneNumber,
                    ActivationCode = activationCode,
                    CommissionId = (from c in db.Commissions
                                    where c.Name == "Tier1"
                                    select c.Percentage).FirstOrDefault()
                };
                var result = await UserManager.CreateAsync(supplier, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(supplier, isPersistent: false, rememberBrowser: false);

                    ApplicationUser user = db.Users.Where(u => u.Email.Equals(model.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (user != null) UserManager.AddToRole(user.Id, "Supplier");

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Supplier");
                }
                AddErrors(result);
            }
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var random = new Random();
            string code = CodeGenerator();
            int randomNumber = random.Next(1, 9);
            code = code + randomNumber.ToString() + "#" + "A";
            var user = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            if (String.IsNullOrEmpty(user.Id))
                RedirectToAction("Index", "Home");

            string hashedNewPassword = UserManager.PasswordHasher.HashPassword(code);
            user.PasswordHash = hashedNewPassword;
            db.SaveChanges();

            //It the body of the email that will be sent to the user after the registration process
            var body = "<p>Dear Valued Customer,</p><p>This is the password that has been sent to you in order to validate your registration on BontoBuy</p>" +
                "<p>Your activation code: {0}</p>";

            var message = new MailMessage();

            //It contains the recipient of the email
            message.To.Add(new MailAddress(model.Email));

            //It contains the email address of BontoBuy
            message.From = new MailAddress("bontobuy@gmail.com");

            //Subject of the mail
            message.Subject = "Register on BontoBuy";

            //Using formatted string the activation code is then added to the body of the email
            message.Body = string.Format(body, code);
            message.IsBodyHtml = true;

            var smtp = new SmtpClient();

            //Use credential of BontoBuy email
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

            //var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), user.p, newPassword);
            //if (result.Succeeded)
            //{
            //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //    if (user != null)
            //    {
            //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //    }
            //    return RedirectToAction("Index", "Customer", new { Message = ManageMessageId.ChangePasswordSuccess });
            //}
            //AddErrors(result);

            //return View(model);

            //if (ModelState.IsValid)
            //{
            //    var user = await UserManager.FindByNameAsync(model.Email);
            //    if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
            //    {
            //        // Don't reveal that the user does not exist or is not confirmed
            //        return View("ForgotPasswordConfirmation");
            //    }

            //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //    // Send an email with this link
            //    // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            //    // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //    // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            //    // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            //}

            //// If we got this far, something failed, redisplay form
            //return View(model);

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });

                case SignInStatus.Failure:
                default:

                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrationLogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("RegistrationLogin", "Account");
        }

        public ActionResult PerformLogoff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult RegistrationLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrationLogin(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            if (result.ToString() == "Success")
            {
                var userId = db.Users.Where(x => x.Email == model.Email).Select(x => x.Id).Single();
                var userProfile = (from u in db.Users
                                   where u.Id == userId
                                   select u.ActivationCode).FirstOrDefault();
                var RolesForUser = await UserManager.GetRolesAsync(userId);
                string password = db.Users.Where(x => x.Email == model.Email)
                                     .Select(x => x.PasswordHash)
                                     .Single();
                bool passwordMatches = Crypto.VerifyHashedPassword(password, model.Password);

                //string requestedUrl = Session["InitialRequest"] as string;
                string requestedUrl = returnUrl;
                if (userId != null && passwordMatches == true)
                {
                    switch (RolesForUser[0].ToString())
                    {
                        case "Supplier":
                            if (userProfile != null)
                            {
                                return RedirectToAction("ActivateAccount");
                            }
                            if (String.IsNullOrWhiteSpace(requestedUrl))
                            {
                                return RedirectToAction("Index", "Supplier");
                            }
                            return Redirect(requestedUrl);

                        case "Admin":
                            return RedirectToAction("Index", "Admin");

                        case "Customer":
                            if (userProfile != null)
                            {
                                return RedirectToAction("ActivateAccount");
                            }

                            if (String.IsNullOrWhiteSpace(requestedUrl))
                            {
                                return RedirectToAction("Index", "Customer");
                            }

                            return Redirect(requestedUrl);

                        //return RedirectToAction("Index", "Customer");
                    }
                }
            }

            switch (result)
            {
                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion Helpers

        public enum ManageMessageId
        {
            ActivationSuccess,
            ActivationFailure,
            Error
        }
    }
}