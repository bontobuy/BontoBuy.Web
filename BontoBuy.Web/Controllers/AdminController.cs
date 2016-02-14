using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BontoBuy.Web.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        public ActionResult RetrievePendingModels()
        {
            var pendingModels = from m in db.Models
                                where m.Status == "Pending"
                                select m;

            return View(pendingModels);
        }

        public ActionResult EditModelStatus(int id)
        {
            int modelId = id;

            ViewBag.StatusId = new SelectList(db.Categories.Where(x => x.Status == "Active"), "CategoryId", "Description");
            return null;
        }
    }
}