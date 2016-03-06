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
                                         select u.LastName).FirstOrDefault(),
                        Street = (from d in db.Deliveries
                                  where d.OrderId == id
                                  select d.Street).FirstOrDefault(),

                        City = (from d in db.Deliveries
                                where d.OrderId == id
                                select d.City).FirstOrDefault(),

                        Zipcode = (from d in db.Deliveries
                                   where d.OrderId == id
                                   select d.Zipcode).FirstOrDefault(),

                        Quantity = (from o in db.Orders
                                    where o.OrderId == id
                                    select o.Quantity).FirstOrDefault(),

                        UnitPrice = (from o in db.Orders
                                     where o.OrderId == id
                                     select o.UnitPrice).FirstOrDefault()
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

        public ActionResult CustomerRetrieveDeliveryAddress()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (User.IsInRole("Customer"))
            {
                var records = db.DeliveryAddresses.Where(x => x.UserId == userId);
                var addressList = new List<DeliveryAddressActionViewModel>();
                foreach (var item in records)
                {
                    var addressItem = new DeliveryAddressActionViewModel()
                    {
                        DeliveryAddressId = item.DeliveryAddressId,
                        UserId = item.UserId,
                        CustomerId = item.CustomerId,
                        Street = item.Street,
                        City = item.City,
                        Zipcode = item.Zipcode,
                        Status = item.Status
                    };
                    addressList.Add(addressItem);
                }
                return View(addressList);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CustomerGetDeliveryAddress(int id)
        {
            var record = db.DeliveryAddresses.Where(x => x.DeliveryAddressId == id).FirstOrDefault();
            if (record == null)
                return RedirectToAction("Home", "Error404");

            return View(record);
        }

        public ActionResult CustomerCreateDeliveryAddress()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }
            var deliveryAddress = new DeliveryAddressActionViewModel();
            ViewBag.DeliveryAddressStatusId = new SelectList(db.DeliveryAddressStatuses, "DeliveryAddressStatusId", "Status");
            return View(deliveryAddress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerCreateDeliveryAddress(DeliveryAddressActionViewModel item)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                ViewBag.DeliveryAddressStatusId = new SelectList(db.DeliveryAddressStatuses, "DeliveryAddressStatusId", "Status", item.DeliveryAddressStatusId);
                var deliveryAddress = new DeliveryAddressViewModel()
                {
                    UserId = userId,
                    CustomerId = (from c in db.Customers
                                  where c.Id == userId
                                  select c.CustomerId).FirstOrDefault(),
                    Street = item.Street,
                    City = item.City,
                    Zipcode = item.Zipcode,
                    Status = (from ds in db.DeliveryAddressStatuses
                              where ds.DeliveryAddressStatusId == item.DeliveryAddressStatusId
                              select ds.Status).FirstOrDefault()
                };
                if (deliveryAddress.Status == "Default")
                {
                    var otherAddresses = db.DeliveryAddresses.Where(x => x.UserId == userId).ToList();
                    if (otherAddresses != null)
                    {
                        foreach (var obj in otherAddresses)
                        {
                            obj.Status = "Other";
                        }
                    }
                }
                db.DeliveryAddresses.Add(deliveryAddress);
                db.SaveChanges();

                return RedirectToAction("CustomerRetrieveDeliveryAddress", "Customer");
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CustomerUpdateDeliveryAddress(int id = 1)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }
            var record = db.DeliveryAddresses.Where(x => x.UserId == userId).FirstOrDefault();
            if (record == null)
                return RedirectToAction("Home", "Error404");

            var recordToUpdate = new DeliveryAddressActionViewModel()
            {
                City = record.City,
                CustomerId = record.CustomerId,
                DeliveryAddressId = record.DeliveryAddressId,
                Status = record.Status,
                Street = record.Street,
                Zipcode = record.Zipcode,
                DeliveryAddressStatusId = 2,
                UserId = record.UserId
            };

            //Need to test !!
            ViewBag.DeliveryAddressStatus = new SelectList(db.DeliveryAddressStatuses, "DeliveryAddressStatusId", "Status");
            Session["Data"] = recordToUpdate;
            return View(recordToUpdate);
        }

        [HttpPost]

        // public ActionResult CustomerUpdateDeliveryAddress(DeliveryAddressActionViewModel item)
        public ActionResult CustomerUpdateDeliveryAddress()
        {
            var recordToUpdate = Session["Data"] as DeliveryAddressActionViewModel;
            var userId = User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid && recordToUpdate != null)
            {
                ViewBag.DeliveryAddressStatus = new SelectList(db.DeliveryAddressStatuses, "DeliveryAddressStatusId", "Status", recordToUpdate.DeliveryAddressStatusId);

                var updatedRecord = db.DeliveryAddresses.Where(x => x.DeliveryAddressId == recordToUpdate.DeliveryAddressId).FirstOrDefault();
                updatedRecord.CustomerId = updatedRecord.CustomerId;
                updatedRecord.UserId = updatedRecord.UserId;
                updatedRecord.Zipcode = updatedRecord.Zipcode;
                updatedRecord.City = updatedRecord.City;
                updatedRecord.Street = updatedRecord.Street;
                updatedRecord.Status = (from da in db.DeliveryAddressStatuses
                                        where da.DeliveryAddressStatusId == recordToUpdate.DeliveryAddressStatusId
                                        select da.Status).FirstOrDefault();
                if (updatedRecord.Status == "Default")
                {
                    var allDelAddress = db.DeliveryAddresses.Where(x => x.Status == "Default" && x.UserId == userId);
                    foreach (var obj in allDelAddress)
                    {
                        obj.Status = "Other";
                    }
                }
                if (updatedRecord.Status == "Other")
                {
                    var allDelAddress = db.DeliveryAddresses.Where(x => x.Status == "Default" && x.UserId == userId && x.DeliveryAddressId != updatedRecord.DeliveryAddressId);
                    if (!allDelAddress.Any())
                    {
                        var firstDelAddress = db.DeliveryAddresses.OrderByDescending(x => x.DeliveryAddressId).FirstOrDefault();
                        firstDelAddress.Status = "Default";
                    }
                }
                db.SaveChanges();

                return RedirectToAction("CustomerRetrieveDeliveryAddress", "Customer");
            }

            return RedirectToAction("Home", "Error404");
        }
        public ActionResult CustomerCreateReturns()
        {
            //Refer to ReturnController Create
            return null;
        }
    }
}