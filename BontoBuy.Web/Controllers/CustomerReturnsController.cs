using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class CustomerReturnsController : NotificationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public enum ManageMessageId
        {
            AddReturnSuccess,
            Error
        }

        // GET: CustomerReturns
        public ActionResult RetrieveReturns(ManageMessageId? message, int? page)
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var records = (from r in db.Returns
                           join o in db.Orders on r.OrderId equals o.OrderId
                           where o.CustomerUserId == userId
                           select r).OrderBy(o => o.ReturnId);
            if (records == null)
                return RedirectToAction("Home", "Error404");

            ViewBag.StatusMessage =
               message == ManageMessageId.AddReturnSuccess ? "Your return has been created."
               : message == ManageMessageId.Error ? "An error has occurred."
               : "";

            GetCustomerReturnNotification();
            GetCustomerNotification();
            ViewBag.Title = "List of your Returns";

            var pageNumber = page ?? 1;
            var pageOfProducts = records.ToPagedList(pageNumber, 10);
            ViewBag.pageOfProducts = pageOfProducts;

            return View();
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
            Session["CustomerReturn"] = record;

            if (record == null)
                return RedirectToAction("Home", "Error404");

            GetCustomerReturnNotification();
            GetCustomerNotification();
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

            GetCustomerReturnNotification();
            GetCustomerNotification();
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
                DtUpdated = DateTime.UtcNow,
                Notification = "Supplier",
                HasApproved = false
            };
            db.Returns.Add(newReturn);
            db.SaveChanges();

            var orderRecord = db.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
            orderRecord.HasReturn = true;
            db.SaveChanges();

            return RedirectToAction("RetrieveReturns", "CustomerReturns", new { message = ManageMessageId.AddReturnSuccess });
        }

        [HttpPost]
        public ActionResult UpdatedReturns(int? page)
        {
            var userId = User.Identity.GetUserId();
            var updatedReturns = (from r in db.Returns
                                  join o in db.Orders on r.OrderId equals o.OrderId
                                  where o.CustomerUserId == userId && r.Notification == "Customer"
                                  select r).ToList();
            if (updatedReturns.Count() <= 0)
            {
                return RedirectToAction("RetrieveReturns");
            }
            foreach (var item in updatedReturns)
            {
                item.Notification = null;
            }
            db.SaveChanges();

            GetCustomerReturnNotification();
            GetCustomerNotification();
            ViewBag.Title = "List of your Updated Returns";
            ViewBag.CustomerReturnStatus = "Your return status has been updated. Please check it.";

            var pageNumber = page ?? 1;
            var pageOfProducts = updatedReturns.ToPagedList(pageNumber, 10);
            ViewBag.pageOfProducts = pageOfProducts;

            return View("RetrieveReturns");
        }

        public ActionResult ViewReturnPdf()
        {
            var customerReturn = Session["CustomerReturn"] as ReturnViewModel;
            return new ViewAsPdf("ViewReturnPdf", customerReturn);
        }
    }
}