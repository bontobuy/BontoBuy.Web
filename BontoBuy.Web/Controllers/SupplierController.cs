using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;

namespace BontoBuy.Web.Controllers
{
    public class SupplierController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Supplier
        public ActionResult Index()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        return View();
                    }

                    return RedirectToAction("LoginSupplier", "Account");
                }
                return RedirectToAction("LoginSupplier", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult AddProduct()
        {
            return View();
        }
    }
}