using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Rotativa;

namespace BontoBuy.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerRetrieveOrders()
        {
            try
            {
                var initialRequest = Request.Url.AbsoluteUri.ToString();
                if (!String.IsNullOrWhiteSpace(initialRequest))
                    Session["InitialRequest"] = initialRequest;

                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Customer"))
                {
                    var orderList = new List<CustomerRetrieveOrdersViewModel>();
                    var records = from o in db.Orders
                                  where o.CustomerUserId == userId
                                  select o;
                    foreach (var item in records)
                    {
                        var orderItem = new CustomerRetrieveOrdersViewModel()
                        {
                            OrderId = item.OrderId,
                            ModelId = item.ModelId,
                            ModelNumber = (from m in db.Models
                                           where m.ModelId == item.ModelId
                                           select m.ModelNumber).FirstOrDefault(),
                            SupplierName = (from c in db.Suppliers
                                            where c.SupplierId == item.SupplierId
                                            select c.Name).FirstOrDefault(),
                            Status = item.Status,
                            DtCreated = item.DtCreated
                        };
                        orderList.Add(orderItem);
                    }
                    return View(orderList);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult CustomerGetOrder(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (User.IsInRole("Customer"))
                {
                    var record = (from o in db.Orders
                                  where o.OrderId == id
                                  select o).FirstOrDefault();

                    var customerOrder = new CustomerGetOrderViewModel()
                    {
                        OrderId = record.OrderId,
                        SupplierName = (from c in db.Suppliers
                                        where c.SupplierId == record.SupplierId
                                        select c.Name).FirstOrDefault(),
                        ModelNumber = (from m in db.Models
                                       where m.ModelId == record.ModelId
                                       select m.ModelNumber).FirstOrDefault(),
                        Status = record.Status,
                        Total = record.Total,
                        GrandTotal = record.Total,
                        DtCreated = record.DtCreated,
                        ExpectedDeliveryDate = record.ExpectedDeliveryDate,
                        RealDeliveryDate = record.RealDeliveryDate,
                        CustomerFName = (from u in db.Users
                                         where u.Id == userId
                                         select u.FirstName).FirstOrDefault(),
                        CustomerLName = (from u in db.Users
                                         where u.Id == userId
                                         select u.LastName).FirstOrDefault()
                    };
                    Session["CustomerOrder"] = customerOrder;
                    return View(customerOrder);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ViewOrderPdf()
        {
            var customerOrder = Session["CustomerOrder"] as CustomerGetOrderViewModel;
            Session.Remove("CustomerOrder");

            return new ViewAsPdf("ViewOrderPdf", customerOrder);
        }
    }
}