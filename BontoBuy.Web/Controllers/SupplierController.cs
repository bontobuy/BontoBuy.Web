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
    public class SupplierController : NotificationController
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
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    int cancelledOrders = db.Orders.Where(o => o.Status == "Inactive" && o.SupplierUserId == userId).ToList().Count();
                    ViewBag.CancelledOrdersCount = cancelledOrders;

                    var orderList = new List<SupplierRetrieveOrdersViewModel>();
                    var orderRecords = from o in db.Orders
                                       where o.SupplierUserId == userId
                                       select o;
                    foreach (var item in orderRecords.Take(10))
                    {
                        var orderItem = new SupplierRetrieveOrdersViewModel()
                        {
                            OrderId = item.OrderId,
                            ModelId = item.ModelId,
                            ModelNumber = (from m in db.Models
                                           where m.ModelId == item.ModelId
                                           select m.ModelNumber).FirstOrDefault(),
                            CustomerName = (from c in db.Customers
                                            where c.CustomerId == item.CustomerId
                                            select c.Name).FirstOrDefault(),
                            Status = item.Status,
                            DtCreated = item.DtCreated
                        };
                        orderList.Add(orderItem);
                    }

                    ViewBag.OrderTransactions = orderList.OrderByDescending(o => o.DtCreated);

                    var returnRecords = from r in db.Returns
                                        join o in db.Orders on r.OrderId equals o.OrderId
                                        where o.SupplierUserId == userId
                                        select r;
                    if (returnRecords == null)
                        return RedirectToAction("Index", "Error404");

                    ViewBag.ReturnTransactions = returnRecords.Take(10);

                    GetSupplierNotification();
                    GetSupplierReturnNotification();
                    return View();
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult AddProduct()
        {
            GetSupplierNotification();
            return View();
        }

        public ActionResult SupplierRetrieveModels(string searchString, ManageMessageId? message)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        var getSupplier = (from s in db.Suppliers
                                           where s.Id == userId
                                           select s).FirstOrDefault();

                        int supplierId = getSupplier.SupplierId;

                        var retrieveModels = (from m in db.Models
                                              join ms in db.ModelSpecs on m.ModelId equals ms.ModelId
                                              join s in db.Suppliers on ms.SupplierId equals s.SupplierId
                                              where ms.SupplierId == supplierId
                                              where m.ModelNumber.Contains(searchString)
                                              select m).Distinct();

                        var modelList = new List<SupplierRetrieveModelsViewModel>();

                        foreach (var obj in retrieveModels)
                        {
                            var model = new SupplierRetrieveModelsViewModel()
                            {
                                SupplierId = supplierId,
                                ModelId = obj.ModelId,
                                ModelNumber = obj.ModelNumber,
                                BrandName = (from b in db.Brands
                                             join m in db.Models on b.BrandId equals m.BrandId
                                             where b.BrandId == obj.BrandId
                                             select b.Name).FirstOrDefault(),
                                ImageUrl = (from ph in db.Photos
                                            join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                            join m in db.Models on pm.ModelId equals m.ModelId
                                            where pm.ModelId == obj.ModelId
                                            select ph.ImageUrl).FirstOrDefault(),
                                Status = obj.Status
                            };

                            modelList.Add(model);
                        }

                        ViewBag.StatusMessage =
                            message == ManageMessageId.AddModelSuccess ? "You just added a model successfully."
                            : message == ManageMessageId.Error ? "An error has occurred."
                            : message == ManageMessageId.UpdateOrderStatus ? "You just updated the order status."
                             : "";

                        GetSupplierReturnNotification();
                        GetSupplierNotification();
                        return View(modelList);
                    }
                    if (String.IsNullOrEmpty(searchString))
                    {
                        var getSupplier = (from s in db.Suppliers
                                           where s.Id == userId
                                           select s).FirstOrDefault();

                        int supplierId = getSupplier.SupplierId;

                        var retrieveModels = (from m in db.Models
                                              join ms in db.ModelSpecs on m.ModelId equals ms.ModelId
                                              join s in db.Suppliers on ms.SupplierId equals s.SupplierId
                                              where ms.SupplierId == supplierId
                                              select m).Distinct();

                        var modelList = new List<SupplierRetrieveModelsViewModel>();

                        foreach (var obj in retrieveModels)
                        {
                            var model = new SupplierRetrieveModelsViewModel()
                            {
                                SupplierId = supplierId,
                                ModelId = obj.ModelId,
                                ModelNumber = obj.ModelNumber,
                                BrandName = (from b in db.Brands
                                             join m in db.Models on b.BrandId equals m.BrandId
                                             where b.BrandId == obj.BrandId
                                             select b.Name).FirstOrDefault(),
                                ImageUrl = (from ph in db.Photos
                                            join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                            join m in db.Models on pm.ModelId equals m.ModelId
                                            where pm.ModelId == obj.ModelId
                                            select ph.ImageUrl).FirstOrDefault(),
                                Status = obj.Status
                            };

                            modelList.Add(model);
                        }

                        GetSupplierReturnNotification();
                        GetSupplierNotification();
                        return View(modelList.OrderBy(m => m.Status));
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult SupplierGetModelSpec(int id)
        {
            try
            {
                int modelId = id;

                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    var getSupplier = (from s in db.Suppliers
                                       where s.Id == userId
                                       select s).FirstOrDefault();

                    var modelSpecList = (from ms in db.ModelSpecs
                                         where ms.SupplierId == getSupplier.SupplierId
                                         && ms.ModelId == modelId
                                         select ms).ToList();

                    var modelSpecVM = new List<SupplierGetModelSpecViewModel>();

                    foreach (var obj in modelSpecList)
                    {
                        var model = new SupplierGetModelSpecViewModel()
                        {
                            SupplierId = getSupplier.SupplierId,
                            ModelId = modelId,
                            Value = obj.Value,
                            Description = (from s in db.Specifications
                                           join ms in db.ModelSpecs on s.SpecificationId equals ms.SpecificationId
                                           where s.SpecificationId == obj.SpecificationId
                                           select s.Description).FirstOrDefault()
                        };
                        ViewBag.ModelSpec = db.Models.Find(modelId).ModelNumber.ToString();
                        modelSpecVM.Add(model);
                    }

                    GetSupplierReturnNotification();
                    GetSupplierNotification();
                    return View(modelSpecVM);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult SupplierRetrieveOrders(ManageMessageId? message)
        {
            try
            {
                Session.Remove("InitialRequest");

                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    var initialRequest = Request.Url.AbsoluteUri.ToString();
                    if (!String.IsNullOrWhiteSpace(initialRequest))
                        Session["InitialRequest"] = initialRequest;
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    var orderList = new List<SupplierRetrieveOrdersViewModel>();
                    var records = from o in db.Orders
                                  where o.SupplierUserId == userId
                                  select o;
                    foreach (var item in records)
                    {
                        var orderItem = new SupplierRetrieveOrdersViewModel()
                        {
                            OrderId = item.OrderId,
                            ModelId = item.ModelId,
                            ModelNumber = (from m in db.Models
                                           where m.ModelId == item.ModelId
                                           select m.ModelNumber).FirstOrDefault(),
                            CustomerName = (from c in db.Customers
                                            where c.CustomerId == item.CustomerId
                                            select c.Name).FirstOrDefault(),
                            Status = item.Status,
                            DtCreated = item.DtCreated
                        };
                        orderList.Add(orderItem);
                    }

                    GetSupplierReturnNotification();
                    GetSupplierNotification();
                    ViewBag.Title = "List of your Orders";

                    ViewBag.StatusMessage =
                           message == ManageMessageId.ConfirmationFailure ? "Your Confirmation code did not match with the Customer code."
                           : message == ManageMessageId.Error ? "An error has occurred."
                           : message == ManageMessageId.UpdateOrderStatus ? "You just updated the order status."
                           : message == ManageMessageId.OrderDeliveredSuccess ? "You just confirmed that your order has been delivered."
                            : "";
                    return View(orderList);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult SupplierGetOrder(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (User.IsInRole("Supplier"))
                {
                    var record = (from o in db.Orders
                                  where o.OrderId == id
                                  select o).FirstOrDefault();

                    var supplierOrder = new SupplierGetOrderViewModel()
                    {
                        OrderId = record.OrderId,
                        CustomerName = (from c in db.Customers
                                        where c.CustomerId == record.CustomerId
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
                        SupplierName = (from u in db.Users
                                        where u.Id == userId
                                        select u.Name).FirstOrDefault(),

                        Street = (from d in db.Deliveries
                                  where d.OrderId == id
                                  select d.Street).FirstOrDefault(),

                        City = (from d in db.Deliveries
                                where d.OrderId == id
                                select d.City).FirstOrDefault(),

                        Zipcode = (from d in db.Deliveries
                                   where d.OrderId == id
                                   select d.Zipcode).FirstOrDefault(),

                        PhoneNumber = (from u in db.Users
                                       join o in db.Orders on u.Id equals o.CustomerUserId
                                       where o.OrderId == id
                                       select u.PhoneNumber).FirstOrDefault()
                    };
                    Session["SupplierOrder"] = supplierOrder;
                    GetSupplierReturnNotification();
                    GetSupplierNotification();
                    return View(supplierOrder);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult Test()
        {
            return Content("TExt");
        }

        public ActionResult SupplierViewOrderPdf()
        {
            var supplierOrder = Session["SupplierOrder"] as SupplierGetOrderViewModel;

            //Session.Remove("SupplierOrder");
            return new ViewAsPdf("SupplierViewOrderPdf", supplierOrder);
        }

        public ActionResult SupplierEditOrder(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (User.IsInRole("Supplier"))
                {
                    var record = (from o in db.Orders
                                  where o.OrderId == id
                                  select o).FirstOrDefault();
                    var order = new SupplierUpdateOrderViewModel()
                    {
                        OrderId = record.OrderId,
                        DtCreated = record.DtCreated,
                        ExpectedDeliveryDate = record.ExpectedDeliveryDate,
                        RealDeliveryDate = record.RealDeliveryDate,
                        Total = record.Total,
                        GrandTotal = record.GrandTotal,
                        CustomerName = (from c in db.Customers
                                        where c.CustomerId == record.CustomerId
                                        select c.Name).FirstOrDefault(),
                        Status = record.Status
                    };

                    ViewBag.OrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "Name");

                    GetSupplierReturnNotification();
                    GetSupplierNotification();
                    return View(order);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult SupplierEditOrder(SupplierUpdateOrderViewModel item, string ConfirmationCode)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (User.IsInRole("Supplier"))
                {
                    var order = (from o in db.Orders
                                 where o.OrderId == item.OrderId
                                 select o).FirstOrDefault();

                    //ViewBag.OrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "Name", item.OrderStatusId);

                    //order.Status = (from os in db.OrderStatuses
                    //                where os.OrderStatusId == item.OrderStatusId
                    //                select os.Name).FirstOrDefault();

                    string orderStatus = order.Status.ToString();

                    if (orderStatus == "Active")
                    {
                        order.Status = "Processing";
                    }

                    if (orderStatus == "Processing")
                    {
                        order.Status = "In Transit";
                    }

                    if (orderStatus == "In Transit")
                    {
                        string SupplierCode = ConfirmationCode.ToString();
                        string CustomerCode = (from o in db.Orders
                                               where o.OrderId == order.OrderId
                                               select o.ConfirmationCode).FirstOrDefault();

                        if (SupplierCode == CustomerCode)
                        {
                            order.Status = "Delivered";
                            db.SaveChanges();
                            return RedirectToAction("SupplierRetrieveOrders", "Supplier", new { message = ManageMessageId.OrderDeliveredSuccess });
                        }
                        return RedirectToAction("SupplierRetrieveOrders", "Supplier", new { message = ManageMessageId.ConfirmationFailure });
                    }

                    order.Notification = "Customer";
                    db.SaveChanges();

                    GetSupplierReturnNotification();
                    GetSupplierNotification();
                    return RedirectToAction("SupplierRetrieveOrders", "Supplier", new { message = ManageMessageId.UpdateOrderStatus });
                }
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Error404", "Home");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult SupplierRetrieveReturns(ManageMessageId? message)
        {
            //View ReturnViewModel

            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var records = from r in db.Returns
                          join o in db.Orders on r.OrderId equals o.OrderId
                          where o.SupplierUserId == userId
                          select r;
            if (records == null)
                return RedirectToAction("Home", "Error404");

            ViewBag.StatusMessage =
                message == ManageMessageId.UpdateReturnStatus ? "You successfully updated Return Details."
                : message == ManageMessageId.Error ? "An error occured."
                : "";

            ViewBag.Title = "List of your Returns";
            GetSupplierReturnNotification();
            GetSupplierNotification();
            return View(records);
        }

        public ActionResult SupplierGetReturn(int id)
        {
            //View ReturnViewModel

            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var record = (from r in db.Returns
                          join o in db.Orders on r.OrderId equals o.OrderId
                          where o.SupplierUserId == userId &&
                          r.ReturnId == id
                          select r).FirstOrDefault();

            if (record == null)
                return RedirectToAction("Home", "Error404");

            GetSupplierReturnNotification();
            GetSupplierNotification();
            return View(record);
        }

        public ActionResult SupplierEditReturn(int id)
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            //View ReturnActionViewModel
            var record = (from r in db.Returns
                          join o in db.Orders on r.OrderId equals o.OrderId
                          where o.SupplierUserId == userId &&
                          r.ReturnId == id
                          select r).FirstOrDefault();

            var itemToUpdate = new ReturnActionViewModel()
            {
                ReturnId = record.ReturnId,
                OrderId = record.OrderId,
                ReturnDate = record.ReturnDate,
                ReturnMethod = record.ReturnMethod,
                Reason = record.Reason,
                Status = record.Status,
                DtCreated = record.DtCreated,
                DtUpdated = record.DtUpdated
            };

            //Refer to Product Controller for View
            ViewBag.ReturnStatusId = new SelectList(db.ReturnStatuses, "ReturnStatusId", "Status");
            GetSupplierReturnNotification();
            GetSupplierNotification();
            return View(itemToUpdate);
        }

        [HttpPost]
        public ActionResult SupplierEditReturn(ReturnActionViewModel item)
        {
            try
            {
                var testReturnId = item;

                //Refer to Product Controller for View
                ViewBag.ReturnStatusId = new SelectList(db.ReturnStatuses, "ReturnStatusId", "Status", item.ReturnStatusId);

                var recordToUpdate = db.Returns.Where(r => r.ReturnId == item.ReturnId).FirstOrDefault();
                recordToUpdate.ReturnMethod = item.ReturnMethod;
                recordToUpdate.ReturnDate = item.ReturnDate;
                recordToUpdate.Status = (from rs in db.ReturnStatuses
                                         where rs.ReturnStatusId == item.ReturnStatusId
                                         select rs.Status).FirstOrDefault();
                recordToUpdate.DtUpdated = DateTime.UtcNow;
                recordToUpdate.Notification = "Customer";

                //var itemToUpdate = new ReturnActionViewModel()
                //{
                //    ReturnId = item.ReturnId,
                //    OrderId = item.OrderId,
                //    ReturnDate = item.ReturnDate,
                //    ReturnMethod = item.ReturnMethod,

                //    Reason = item.Reason,
                //    Status = (from rs in db.ReturnStatuses
                //              where rs.ReturnStatusId == item.ReturnStatusId
                //              select rs.Status).FirstOrDefault(),

                //    DtCreated = item.DtCreated,
                //    DtUpdated = DateTime.UtcNow
                //};
                db.SaveChanges();
                GetSupplierReturnNotification();
                GetSupplierNotification();
                return RedirectToAction("SupplierRetrieveReturns", "Supplier", new { message = ManageMessageId.UpdateReturnStatus });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CancelledOrders()
        {
            var userId = User.Identity.GetUserId();
            var cancelledOrders = db.Orders.Where(o => o.SupplierUserId == userId && o.Status == "Inactive").ToList();
            if (cancelledOrders == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in cancelledOrders)
            {
                item.Notification = null;
            }
            db.SaveChanges();

            var cancelledOrderList = new List<SupplierRetrieveOrdersViewModel>();

            foreach (var item in cancelledOrders)
            {
                var orderItem = new SupplierRetrieveOrdersViewModel()
                {
                    OrderId = item.OrderId,
                    ModelId = item.ModelId,
                    ModelNumber = (from m in db.Models
                                   where m.ModelId == item.ModelId
                                   select m.ModelNumber).FirstOrDefault(),
                    CustomerName = (from c in db.Customers
                                    where c.CustomerId == item.CustomerId
                                    select c.Name).FirstOrDefault(),
                    Status = item.Status,
                    DtCreated = item.DtCreated
                };
                cancelledOrderList.Add(orderItem);
            }
            GetSupplierReturnNotification();
            GetSupplierNotification();
            ViewBag.Title = "List of your Cancelled Orders";
            return View("SupplierRetrieveOrders", cancelledOrderList.OrderByDescending(o => o.DtCreated));
        }

        [HttpPost]
        public ActionResult NewOrders()
        {
            var userId = User.Identity.GetUserId();
            var newOrders = db.Orders.Where(o => o.SupplierUserId == userId && o.Notification == "Supplier").ToList();
            if (newOrders == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in newOrders)
            {
                item.Notification = null;
            }
            db.SaveChanges();

            var newOrderList = new List<SupplierRetrieveOrdersViewModel>();

            foreach (var item in newOrders)
            {
                var orderItem = new SupplierRetrieveOrdersViewModel()
                {
                    OrderId = item.OrderId,
                    ModelId = item.ModelId,
                    ModelNumber = (from m in db.Models
                                   where m.ModelId == item.ModelId
                                   select m.ModelNumber).FirstOrDefault(),
                    CustomerName = (from c in db.Customers
                                    where c.CustomerId == item.CustomerId
                                    select c.Name).FirstOrDefault(),
                    Status = item.Status,
                    DtCreated = item.DtCreated
                };
                newOrderList.Add(orderItem);
            }
            GetSupplierReturnNotification();
            GetSupplierNotification();
            ViewBag.Title = "List of your New Orders";
            return View("SupplierRetrieveOrders", newOrderList);
        }

        [HttpPost]
        public ActionResult NewReturns()
        {
            var userId = User.Identity.GetUserId();
            var newReturns = (from r in db.Returns
                              join o in db.Orders on r.OrderId equals o.OrderId
                              where o.SupplierUserId == userId && r.Notification == "Supplier"
                              select r).ToList();
            if (newReturns == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in newReturns)
            {
                item.Notification = null;
            }
            db.SaveChanges();

            GetSupplierReturnNotification();
            GetSupplierNotification();
            ViewBag.Title = "List of your New Returns";
            return View("SupplierRetrieveReturns", newReturns);
        }

        [HttpPost]
        public ActionResult SupplierFilterOrders(int SearchString)
        {
            var userId = User.Identity.GetUserId();
            var records = db.Orders.Where(o => o.OrderId == SearchString && o.SupplierUserId == userId).ToList();
            var orderList = new List<SupplierRetrieveOrdersViewModel>();
            foreach (var item in records)
            {
                var orderItem = new SupplierRetrieveOrdersViewModel()
                {
                    OrderId = item.OrderId,
                    ModelId = item.ModelId,
                    ModelNumber = (from m in db.Models
                                   where m.ModelId == item.ModelId
                                   select m.ModelNumber).FirstOrDefault(),
                    CustomerName = (from c in db.Customers
                                    where c.CustomerId == item.CustomerId
                                    select c.Name).FirstOrDefault(),
                    Status = item.Status,
                    DtCreated = item.DtCreated
                };
                orderList.Add(orderItem);
            }
            ViewBag.Title = "List of your Orders";
            return View("SupplierRetrieveOrders", orderList);
        }

        public ActionResult UpdateOrder(int id)
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (User.IsInRole("Supplier"))
            {
                var order = (from o in db.Orders
                             where o.OrderId == id
                             select o).FirstOrDefault();

                string orderStatus = order.Status.ToString();

                if (orderStatus == "Active")
                {
                    order.Status = "Processing";
                }
                else if (orderStatus == "Processing")
                {
                    order.Status = "In Transit";
                }

                order.Notification = "Customer";
                db.SaveChanges();

                GetSupplierReturnNotification();
                GetSupplierNotification();
                return RedirectToAction("SupplierRetrieveOrders", "Supplier", new { message = ManageMessageId.UpdateOrderStatus });
            }
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Error404", "Home");
        }

        public enum ManageMessageId
        {
            AddModelSuccess,
            UpdateOrderStatus,
            UpdateReturnStatus,
            ConfirmationFailure,
            OrderDeliveredSuccess,
            Error
        }
    }
}