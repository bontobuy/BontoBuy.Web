using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class NotificationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void GetSupplierNotification()
        {
            var userId = User.Identity.GetUserId();
            int SupplierNotification = db.Orders.Where(o => o.SupplierUserId == userId && o.Notification == "Supplier").Count();
            ViewBag.SupplierNotification = SupplierNotification;
        }

        public void GetCustomerNotification()
        {
            var userId = User.Identity.GetUserId();
            int CustomerNotification = db.Orders.Where(o => o.CustomerUserId == userId && o.Notification == "Customer").Count();
            ViewBag.CustomerNotification = CustomerNotification;
        }

        public void GetCustomerReturnNotification()
        {
            var userId = User.Identity.GetUserId();
            int CustomerNotification = (from r in db.Returns
                                        join o in db.Orders on r.OrderId equals o.OrderId
                                        where o.CustomerUserId == userId && r.Notification == "Customer"
                                        select r).Count();
            ViewBag.CustomerReturnNotification = CustomerNotification;
        }

        public void GetSupplierReturnNotification()
        {
            var userId = User.Identity.GetUserId();
            int ReturnNotification = (from r in db.Returns
                                      join o in db.Orders on r.OrderId equals o.OrderId
                                      where o.SupplierUserId == userId && r.Notification == "Supplier"
                                      select r).Count();
            ViewBag.SupplierReturnNotification = ReturnNotification;
        }
    }
}