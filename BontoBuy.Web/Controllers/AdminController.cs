using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            if (User.IsInRole("Supplier"))
            {
                return RedirectToAction("Index", "Supplier");
            }
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}