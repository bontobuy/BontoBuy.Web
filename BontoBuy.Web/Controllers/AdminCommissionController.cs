using BontoBuy.Web.HelperMethods;
using BontoBuy.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class AdminCommissionController : NotificationController
    {
        private IAdminCommissionRepo _repo;
        private ApplicationDbContext db = new ApplicationDbContext();
        private Helper helper = new Helper();

        public AdminCommissionController(IAdminCommissionRepo repository)
        {
            _repo = repository;
        }

        // GET: AdminCommission
        public ActionResult Retrieve()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.Retrieve();
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminCommission/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id < 1)
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Get(id);
                    if (record == null)
                        return RedirectToAction("Home", "Error404");

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(record);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminCommission/Create
        public ActionResult Create()
        {
            return RedirectToAction("Home", "Error404");
        }

        // POST: AdminCommission/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            return RedirectToAction("Home", "Error404");
        }

        // GET: AdminCommission/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id < 1)
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Get(id);
                    if (record == null)
                        return RedirectToAction("Home", "Error404");

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(record);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: AdminCommission/Edit/5
        [HttpPost]
        public ActionResult Update(CommissionViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (item == null)
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Update(item);
                    if (record == null)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("Retrieve");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveDeliveredOrders(int? page, string searchString, ManageMessageId? message)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveDeliveredOrders();
                    if (records == null)
                        return RedirectToAction("Error404", "Home");

                    var recordList = new List<CommissionOrderViewModel>();
                    foreach (var item in records)
                    {
                        var commission = (((from c in db.Commissions
                                            join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                            where mc.ModelId == item.ModelId
                                            select c.Percentage).FirstOrDefault()));

                        var orderItem = new CommissionOrderViewModel()
                        {
                            OrderId = item.OrderId,
                            ModelName = (from m in db.Models
                                         where m.ModelId == item.ModelId
                                         select m.ModelNumber).FirstOrDefault(),

                            SupplierId = item.SupplierId,

                            SupplierName = (from u in db.Users
                                            where u.Id == item.SupplierUserId
                                            select u.Name).FirstOrDefault(),

                            OrderDate = item.DtCreated,
                            OrderAmount = item.UnitPrice,
                            Commission = Convert.ToInt32((commission * item.UnitPrice) / 100),
                            Paid = item.CommissionPaid
                        };
                        if (!String.IsNullOrWhiteSpace(searchString))
                        {
                            var properString = helper.ConvertToTitleCase(searchString);
                            if (orderItem.SupplierName == properString)
                            {
                                recordList.Add(orderItem);
                            }
                        }
                        if (String.IsNullOrWhiteSpace(searchString))
                        {
                            recordList.Add(orderItem);
                        }
                    }

                    //Paging Section
                    var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
                    var pageOfProducts = recordList.GroupBy(c => c.SupplierName).ToDictionary(g => g.Key, g => g.ToList()).ToPagedList(pageNumber, 10); //set the number of records per page
                    ViewBag.pageOfProducts = pageOfProducts;

                    ViewBag.StatusMessage =
                           message == ManageMessageId.UpdateCommissionOrderSuccess ? "You just updated an Order Commission Status."
                           : message == ManageMessageId.error ? "An error has occurred."
                            : "";

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    ViewBag.Title = "Retrieve Unpaid Commission on Orders";
                    return View();
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult UpdateOrderCommission(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    GetNewSupplierActivation();
                    GetNewModelsActivation();

                    var record = db.Orders.Where(o => o.OrderId == id).FirstOrDefault();
                    if (record == null)
                        return RedirectToAction("RetrieveDeliveredOrders");

                    var commission = (((from c in db.Commissions
                                        join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                        where mc.ModelId == record.ModelId
                                        select c.Percentage).FirstOrDefault()));

                    var orderItem = new CommissionOrderViewModel()
                    {
                        OrderId = record.OrderId,
                        ModelName = (from m in db.Models
                                     where m.ModelId == record.ModelId
                                     select m.ModelNumber).FirstOrDefault(),

                        SupplierId = record.SupplierId,

                        SupplierName = (from u in db.Users
                                        where u.Id == record.SupplierUserId
                                        select u.Name).FirstOrDefault(),

                        OrderDate = record.DtCreated,
                        OrderAmount = record.UnitPrice,
                        Commission = Convert.ToInt32((commission * record.UnitPrice) / 100),
                        Paid = record.CommissionPaid
                    };

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(orderItem);
                }
            }
            catch (Exception ex)
            {
                return View("RetrieveDeliveredOrders");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateOrderCommission(CommissionOrderViewModel item)
        {
            if (User.IsInRole("Admin"))
            {
                var record = db.Orders.Where(o => o.OrderId == item.OrderId).FirstOrDefault();
                if (record == null)
                    return RedirectToAction("RetrieveDeliveredOrders", new { message = ManageMessageId.error });

                record.CommissionPaid = true;
                db.SaveChanges();

                GetSupplierReturnNotification();
                GetSupplierNotification();
                return RedirectToAction("RetrieveDeliveredOrders", new { message = ManageMessageId.UpdateCommissionOrderSuccess });
            }

            return RedirectToAction("RetrieveDeliveredOrders", new { message = ManageMessageId.error });
        }

        public ActionResult SearchUnpaidOrders(string searchString, int? page)
        {
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                var properString = helper.ConvertToTitleCase(searchString);
                ViewBag.searchString = properString;

                var record = (from o in db.Orders
                              join m in db.Users on o.SupplierUserId equals m.Id
                              where m.Name == properString && o.Status == "Delivered" && o.CommissionPaid == false
                              select o).ToList();

                if (record == null)
                    return RedirectToAction("RetrieveDeliveredOrders");

                var recordList = new List<CommissionOrderViewModel>();
                foreach (var item in record)
                {
                    var commission = (((from c in db.Commissions
                                        join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                        where mc.ModelId == item.ModelId
                                        select c.Percentage).FirstOrDefault()));

                    var orderItem = new CommissionOrderViewModel()
                    {
                        OrderId = item.OrderId,
                        ModelName = (from m in db.Models
                                     where m.ModelId == item.ModelId
                                     select m.ModelNumber).FirstOrDefault(),

                        SupplierId = item.SupplierId,

                        SupplierName = (from u in db.Users
                                        where u.Id == item.SupplierUserId
                                        select u.Name).FirstOrDefault(),

                        OrderDate = item.DtCreated,
                        OrderAmount = item.UnitPrice,
                        Commission = Convert.ToInt32((commission * item.UnitPrice) / 100),
                        Paid = item.CommissionPaid
                    };

                    var supplierId = db.Users.Where(u => u.Name == properString).FirstOrDefault().Id;
                    Session["SupplierId"] = supplierId;
                    recordList.Add(orderItem);
                }

                //Paging Section
                var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
                var pageOfProducts = recordList.ToPagedList(pageNumber, 10); //set the number of records per page
                ViewBag.pageOfProducts = pageOfProducts;

                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View();
            }
            if (String.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction("RetrieveDeliveredOrders");
            }

            return RedirectToAction("RetrieveDeliveredOrders");
        }

        public ActionResult SearchByDate(Nullable<DateTime> fromDate, Nullable<DateTime> toDate, int? page)
        {
            if (User.IsInRole("Admin"))
            {
                string supplierId = Session["supplierId"] as string;
                ViewBag.searchString = "Unpaid Commission from " + fromDate + " to " + toDate;

                var record = (from o in db.Orders
                              where o.DtCreated <= toDate && o.DtCreated >= fromDate && o.Status == "Delivered" && o.CommissionPaid == false && o.SupplierUserId == supplierId
                              select o).ToList();

                if (record == null)
                    return RedirectToAction("RetrieveDeliveredOrders");

                var recordList = new List<CommissionOrderViewModel>();
                foreach (var item in record)
                {
                    var commission = (((from c in db.Commissions
                                        join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                        where mc.ModelId == item.ModelId
                                        select c.Percentage).FirstOrDefault()));

                    var orderItem = new CommissionOrderViewModel()
                    {
                        OrderId = item.OrderId,
                        ModelName = (from m in db.Models
                                     where m.ModelId == item.ModelId
                                     select m.ModelNumber).FirstOrDefault(),

                        SupplierId = item.SupplierId,

                        SupplierName = (from u in db.Users
                                        where u.Id == item.SupplierUserId
                                        select u.Name).FirstOrDefault(),

                        OrderDate = item.DtCreated,
                        OrderAmount = item.UnitPrice,
                        Commission = Convert.ToInt32((commission * item.UnitPrice) / 100),
                        Paid = item.CommissionPaid
                    };
                    recordList.Add(orderItem);
                }

                //Paging Section
                var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
                var pageOfProducts = recordList.ToPagedList(pageNumber, 10); //set the number of records per page
                ViewBag.pageOfProducts = pageOfProducts;

                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View("SearchUnpaidOrders", recordList);
            }

            return RedirectToAction("RetrieveDeliveredOrders");
        }

        public ActionResult RetrievePaidOrders(string searchString, int? page)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrievePaidOrders();

                    if (records == null)
                        return RedirectToAction("Error404", "Home");

                    var recordList = new List<CommissionOrderViewModel>();
                    foreach (var item in records)
                    {
                        var commission = (((from c in db.Commissions
                                            join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                            where mc.ModelId == item.ModelId
                                            select c.Percentage).FirstOrDefault()));

                        var orderItem = new CommissionOrderViewModel()
                        {
                            OrderId = item.OrderId,
                            ModelName = (from m in db.Models
                                         where m.ModelId == item.ModelId
                                         select m.ModelNumber).FirstOrDefault(),

                            SupplierId = item.SupplierId,

                            SupplierName = (from u in db.Users
                                            where u.Id == item.SupplierUserId
                                            select u.Name).FirstOrDefault(),

                            OrderDate = item.DtCreated,
                            OrderAmount = item.UnitPrice,
                            Commission = Convert.ToInt32((commission * item.UnitPrice) / 100),
                            Paid = item.CommissionPaid
                        };
                        if (!String.IsNullOrWhiteSpace(searchString))
                        {
                            var properString = helper.ConvertToTitleCase(searchString);
                            if (orderItem.SupplierName == properString)
                            {
                                recordList.Add(orderItem);
                            }
                        }
                        if (String.IsNullOrWhiteSpace(searchString))
                        {
                            recordList.Add(orderItem);
                        }
                    }

                    //Paging Section
                    var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
                    var pageOfProducts = recordList.GroupBy(c => c.SupplierName).ToDictionary(g => g.Key, g => g.ToList()).ToPagedList(pageNumber, 10); //set the number of records per page
                    ViewBag.pageOfProducts = pageOfProducts;

                    GetNewSupplierActivation();
                    GetNewModelsActivation();

                    ViewBag.Title = "List of Commission Paid on Orders";
                    return View("RetrieveDeliveredOrders");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult UpdateCommissionOwnedFromOrders(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var record = _repo.UpdateCommissionOwnedFromOrders(id);

                    if (record == null)
                        return RedirectToAction("Error404", "Home");

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(record);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public enum ManageMessageId
        {
            UpdateCommissionOrderSuccess,
            error
        }
    }
}