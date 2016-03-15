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
    public class AdminOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IAdminOrderRepo _repo;

        public AdminOrderController(IAdminOrderRepo repository)
        {
            _repo = repository;
        }

        // GET: AdminOrder
        public ActionResult RetrieveOrders()
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
                    return View(records);

                    //var orderList = new List<AdminRetrieveOrdersViewModel>();
                    //var records = (from o in db.Orders
                    //               select o).ToList();
                    //foreach (var item in records)
                    //{
                    //    var orderItem = new AdminRetrieveOrdersViewModel()
                    //    {
                    //        OrderId = item.OrderId,
                    //        ModelId = item.ModelId,
                    //        CustomerId = item.CustomerId,
                    //        CustomerName = (from u in db.Users
                    //                        join o in db.Orders on u.Id equals o.CustomerUserId
                    //                        where o.OrderId == item.OrderId
                    //                        select u.Name).FirstOrDefault(),

                    //        ModelNumber = (from m in db.Models
                    //                       where m.ModelId == item.ModelId
                    //                       select m.ModelNumber).FirstOrDefault(),
                    //        SupplierName = (from c in db.Suppliers
                    //                        where c.SupplierId == item.SupplierId
                    //                        select c.Name).FirstOrDefault(),
                    //        Status = item.Status,
                    //        DtCreated = item.DtCreated
                    //    };
                    //    orderList.Add(orderItem);
                    //}
                    //return View(orderList);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ExportToExcel()
        {
            _repo.ExportToExcel();
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