using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class CustomerController : NotificationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            AddDeliveryAddressSuccess,
            AddOrderSuccess,
            Error
        }

        // GET: Customer
        public ActionResult Index(ManageMessageId? message)
        {
            string userId = User.Identity.GetUserId();
            if (String.IsNullOrEmpty(userId))
                return RedirectToAction("Error404", "Home");
            var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (User.IsInRole("Customer") && user.ActivationCode == null)
            {
                if (User.IsInRole("Supplier"))
                    return RedirectToAction("Index", "Supplier");

                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index", "Admin");

                ViewBag.StatusMessage =
                    message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                    : message == ManageMessageId.Error ? "An error has occurred."
                    : "";

                GetCustomerReturnNotification();
                GetCustomerNotification();
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CustomerRetrieveOrders(int? page)
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
                            DtCreated = item.DtCreated,
                            HasReturn = item.HasReturn
                        };
                        orderList.Add(orderItem);
                    }

                    ViewBag.Title = "List of your Orders";
                    GetCustomerReturnNotification();
                    GetCustomerNotification();

                    var pageNumber = page ?? 1;
                    var pageOfProducts = orderList.ToPagedList(pageNumber, 10);
                    ViewBag.pageOfProducts = pageOfProducts;

                    return View();
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult CustomerGetOrder(int id, ManageMessageId? message)
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
                                     select o.UnitPrice).FirstOrDefault(),

                        ConfirmationCode = (from o in db.Orders
                                            where o.OrderId == id
                                            select o.ConfirmationCode).FirstOrDefault(),
                        PhoneNumber = (from u in db.Users
                                       where u.Id == userId
                                       select u.PhoneNumber).FirstOrDefault()
                    };
                    Session["CustomerOrder"] = customerOrder;
                    ViewBag.StatusMessage =
                message == ManageMessageId.AddOrderSuccess ? "Your order has been created."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                    GetCustomerReturnNotification();
                    GetCustomerNotification();
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

            //Session.Remove("CustomerOrder");

            return new ViewAsPdf("ViewOrderPdf", customerOrder);
        }

        public ActionResult CustomerRetrieveDeliveryAddress(string returnUrl, ManageMessageId? message, int? page)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (User.IsInRole("Customer"))
            {
                string requestedUrl = returnUrl;

                //if (String.IsNullOrEmpty(requestedUrl))
                //{
                //    return View();
                //}
                Session["InitialRequest"] = requestedUrl;

                var records = db.DeliveryAddresses.Where(x => x.UserId == userId && x.Status != "Archived");
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
                ViewBag.StatusMessage =
                message == ManageMessageId.AddDeliveryAddressSuccess ? "New delivery address successfully added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                GetCustomerReturnNotification();
                GetCustomerNotification();

                var pageNumber = page ?? 1;
                var pageOfProducts = addressList.ToPagedList(pageNumber, 10);
                ViewBag.pageOfProducts = pageOfProducts;

                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CustomerGetDeliveryAddress(int id)
        {
            var record = db.DeliveryAddresses.Where(x => x.DeliveryAddressId == id).FirstOrDefault();
            if (record == null)
                return RedirectToAction("Home", "Error404");

            GetCustomerReturnNotification();
            GetCustomerNotification();
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
            GetCustomerNotification();
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

                GetCustomerReturnNotification();
                GetCustomerNotification();
                return RedirectToAction("CustomerRetrieveDeliveryAddress", "Customer", new { message = ManageMessageId.AddDeliveryAddressSuccess });
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult CustomerUpdateDeliveryAddress(int id)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }
            var record = db.DeliveryAddresses.Where(x => x.UserId == userId && x.DeliveryAddressId == id).FirstOrDefault();
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

                //DeliveryAddressStatusId = 2,
                UserId = record.UserId
            };

            //Need to test !!
            ViewBag.DeliveryAddressStatusId = new SelectList(db.DeliveryAddressStatuses, "DeliveryAddressStatusId", "Status");

            GetCustomerReturnNotification();
            GetCustomerNotification();

            //Session["Data"] = recordToUpdate;
            return View(recordToUpdate);
        }

        [HttpPost]

        // public ActionResult CustomerUpdateDeliveryAddress(DeliveryAddressActionViewModel item)
        public ActionResult CustomerUpdateDeliveryAddress(DeliveryAddressActionViewModel item)
        {
            //var recordToUpdate = Session["Data"] as DeliveryAddressActionViewModel;
            var userId = User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                ViewBag.DeliveryAddressStatusId = new SelectList(db.DeliveryAddressStatuses, "DeliveryAddressStatusId", "Status", item.DeliveryAddressStatusId);

                var updatedRecord = db.DeliveryAddresses.Where(x => x.DeliveryAddressId == item.DeliveryAddressId).FirstOrDefault();

                //updatedRecord.CustomerId = updatedRecord.CustomerId;
                //updatedRecord.UserId = updatedRecord.UserId;
                updatedRecord.Zipcode = item.Zipcode;
                updatedRecord.City = item.City;
                updatedRecord.Street = item.Street;
                updatedRecord.Status = (from da in db.DeliveryAddressStatuses
                                        where da.DeliveryAddressStatusId == item.DeliveryAddressStatusId
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

                string requestedUrl = Session["InitialRequest"] as string;
                if (String.IsNullOrWhiteSpace(requestedUrl))
                {
                    return RedirectToAction("CustomerRetrieveDeliveryAddress", "Customer");
                }
                Session.Remove("InitialRequest");

                GetCustomerReturnNotification();
                GetCustomerNotification();
                return Redirect(requestedUrl);
            }

            return RedirectToAction("Home", "Error404");
        }

        public ActionResult ArchiveDeliveryAddress(int id)
        {
            var userId = User.Identity.GetUserId();
            var record = db.DeliveryAddresses.Where(d => d.DeliveryAddressId == id && d.UserId == userId).FirstOrDefault();
            GetCustomerReturnNotification();
            GetCustomerNotification();
            return View(record);
        }

        [HttpPost]
        public ActionResult ArchiveDeliveryAddress(DeliveryAddressViewModel item)
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
                    var itemId = item.DeliveryAddressId;
                    if (itemId < 1)
                    {
                        RedirectToAction("CustomerRetrieveDeliveryAddresse");
                    }
                    if (item == null)
                    {
                        RedirectToAction("CustomerRetrieveDeliveryAddress");
                    }

                    var record = db.DeliveryAddresses.Where(d => d.DeliveryAddressId == itemId && d.UserId == userId).FirstOrDefault();
                    if (record != null)
                    {
                        record.Status = "Archived";
                        db.SaveChanges();
                    }

                    GetCustomerReturnNotification();
                    GetCustomerNotification();

                    //   return RedirectToAction("Retrieve");
                    return RedirectToAction("CustomerRetrieveDeliveryAddress");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        public ActionResult CustomerCreateReturns()
        {
            //Refer to ReturnController Create
            return null;
        }
        [HttpPost]
        public ActionResult UpdatedOrders(int? page)
        {
            var userId = User.Identity.GetUserId();
            var updatedOrders = db.Orders.Where(o => o.CustomerUserId == userId && o.Notification == "Customer").ToList();
            if (updatedOrders.Count() <= 0)
            {
                return RedirectToAction("CustomerRetrieveOrders");
            }
            foreach (var item in updatedOrders)
            {
                item.Notification = null;
            }
            db.SaveChanges();

            var updatedOrderList = new List<CustomerRetrieveOrdersViewModel>();

            foreach (var item in updatedOrders)
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
                    DtCreated = item.DtCreated,
                    HasReturn = item.HasReturn
                };
                updatedOrderList.Add(orderItem);
            }

            GetCustomerReturnNotification();
            GetCustomerNotification();
            ViewBag.Title = "List of your Updated Orders";
            ViewBag.CustomerOrderStatus = "Your order status has been updated. Please check it.";

            var pageNumber = page ?? 1;
            var pageOfProducts = updatedOrderList.ToPagedList(pageNumber, 10);
            ViewBag.pageOfProducts = pageOfProducts;
            return View("CustomerRetrieveOrders");
        }
    }
}