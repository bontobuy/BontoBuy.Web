using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class CustomerReturnsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public enum ManageMessageId
        {
            AddReturnSuccess,
            Error
        }

        // GET: CustomerReturns
        public ActionResult RetrieveReturns(ManageMessageId? message)
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var records = from r in db.Returns
                          join o in db.Orders on r.OrderId equals o.OrderId
                          where o.CustomerUserId == userId
                          select r;
            if (records == null)
                return RedirectToAction("Home", "Error404");

            ViewBag.StatusMessage =
               message == ManageMessageId.AddReturnSuccess ? "Your return has been created."
               : message == ManageMessageId.Error ? "An error has occurred."
               : "";
            return View(records);
        }

        public ActionResult GetReturns(int id)
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var record = (from r in db.Returns
                          join o in db.Orders on r.OrderId equals o.OrderId
                          where o.CustomerUserId == userId &&
                          r.ReturnId == id
                          select r).FirstOrDefault();

            if (record == null)
                return RedirectToAction("Home", "Error404");

            return View(record);
        }

        public ActionResult CreateReturn(int id)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }
            var newReturn = new ReturnViewModel();
            Session["ReturnOrderId"] = id;
            ViewBag.OrderId = id;
            return View(newReturn);
        }

        [HttpPost]
        public ActionResult CreateReturn(ReturnViewModel item)
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int orderId = Convert.ToInt32(Session["ReturnOrderId"]);
            var newReturn = new ReturnViewModel()
            {
                OrderId = orderId,
                Reason = item.Reason,
                Status = "Pending",
                DtCreated = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow,
                DtUpdated = DateTime.UtcNow
            };
            db.Returns.Add(newReturn);
            db.SaveChanges();

            return RedirectToAction("RetrieveReturns", "CustomerReturns", new { message = ManageMessageId.AddReturnSuccess });
        }
    }
}