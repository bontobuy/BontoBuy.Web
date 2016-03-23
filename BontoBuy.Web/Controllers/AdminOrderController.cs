using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using Rotativa;

namespace BontoBuy.Web.Controllers
{
    public class AdminOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IAdminOrderRepo _repo;

        public AdminOrderController(IAdminOrderRepo repository)
        {
            _repo = repository;
        }

        // GET: AdminOrder
        public ActionResult RetrieveOrders(int? page)
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

                if (User.IsInRole("Admin"))
                {
                    var records = _repo.AdminRetrieveOrders();
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    //Paging Section
                    var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
                    var pageOfProducts = records.ToPagedList(pageNumber, 10); //set the number of records per page
                    ViewBag.pageOfProducts = pageOfProducts;

                    ViewBag.Title = "List Of Orders";
                    return View();
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult SearchOrders(Nullable<DateTime> fromDate, Nullable<DateTime> toDate, int? page)
        {
            try
            {
                if (fromDate == null)
                    fromDate = DateTime.Today;
                if (toDate == null)
                    toDate = DateTime.Today;

                var searchResult = from o in db.Orders
                                   where o.DtCreated >= fromDate
                                   && o.DtCreated <= toDate
                                   select o;

                var itemList = new List<AdminRetrieveOrdersViewModel>();
                foreach (var item in searchResult)
                {
                    var orderItem = new AdminRetrieveOrdersViewModel()
                    {
                        OrderId = item.OrderId,
                        ModelId = item.ModelId,
                        CustomerId = item.CustomerId,
                        CustomerName = (from u in db.Users
                                        join o in db.Orders on u.Id equals o.CustomerUserId
                                        where o.OrderId == item.OrderId
                                        select u.Name).FirstOrDefault(),

                        ModelNumber = (from m in db.Models
                                       where m.ModelId == item.ModelId
                                       select m.ModelNumber).FirstOrDefault(),
                        SupplierName = (from c in db.Suppliers
                                        where c.SupplierId == item.SupplierId
                                        select c.Name).FirstOrDefault(),
                        Status = item.Status,
                        DtCreated = item.DtCreated
                    };
                    itemList.Add(orderItem);
                }
                if (itemList == null)
                {
                    ViewBag.Title = "No Order for in that range found.";
                    return View("RetrieveOrders");
                }
                Session["ExcelData"] = itemList;

                //Paging Section
                var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
                var pageOfProducts = itemList.ToPagedList(pageNumber, 10); //set the number of records per page
                ViewBag.pageOfProducts = pageOfProducts;

                ViewBag.Title = "List Of Orders";

                return View("RetrieveOrders");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ExportToExcel()
        {
            var excelData = Session["ExcelData"] as List<AdminRetrieveOrdersViewModel>;
            if (excelData != null)
                _repo.ExportToExcel(excelData);

            Session.Remove("ExcelData");

            return RedirectToAction("RetrieveOrders");
        }

        public ActionResult GetOrder(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (User.IsInRole("Admin"))
                {
                    var record = (from o in db.Orders
                                  where o.OrderId == id
                                  select o).FirstOrDefault();

                    var Order = new AdminGetOrderViewModel()
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
                                         join o in db.Orders on u.Id equals o.CustomerUserId
                                         where o.OrderId == record.OrderId
                                         select u.FirstName).FirstOrDefault(),
                        CustomerLName = (from u in db.Users
                                         join o in db.Orders on u.Id equals o.CustomerUserId
                                         where o.OrderId == record.OrderId
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

                    return View(Order);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}