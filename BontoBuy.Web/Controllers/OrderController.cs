using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewPdf()
        {
            return new ViewAsPdf("Invoice");
        }

        public ActionResult Invoice()
        {
            return View();
        }

        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            var cartList = new List<CartViewModel>()
            {
                new CartViewModel()
                {
                    SupplierId=2,
                    ModelId=1005,
                    UserId= userId,
                    ModelName="Sony Xperia M",
                    UnitPrice=8000,
                    SubTotal=16000,
                    GrandTotal=16500,
                    Quantity=2
                },
                 new CartViewModel()
                {
                    SupplierId=2,
                    ModelId=1005,
                    UserId= userId,
                    ModelName="Xperia T3",
                    UnitPrice=10939,
                    SubTotal=10939,
                    GrandTotal=11000,
                    Quantity=1
                }
            };

            return View(cartList);
        }

        [HttpPost]
        public ActionResult Create(List<CartViewModel> items)
        {
            string userId = User.Identity.GetUserId();

            // To make it work with the session variable just use:
            //var carList = Session["Cart"] as List<CartViewModel>;
            //Comment the code in the DummyProducts region below to make it work

            #region DummyProducts

            var cartList = new List<CartViewModel>()
            {
                new CartViewModel()
                {
                    SupplierId=2,
                    ModelId=1,
                    UserId= userId,
                    ModelName="Sony Xperia M",
                    UnitPrice=8000,
                    SubTotal=16000,
                    GrandTotal=16500,
                    Quantity=2
                },
                 new CartViewModel()
                {
                    SupplierId=2,
                    ModelId=1,
                    UserId= userId,
                    ModelName="Xperia T3",
                    UnitPrice=10939,
                    SubTotal=10939,
                    GrandTotal=11000,
                    Quantity=1
                }
            };

            #endregion DummyProducts

            if (cartList == null)
            {
                return Content("CartList is null");
            }
            if (cartList != null)
            {
                foreach (var item in cartList)
                {
                    var newOrder = new OrderViewModel()
                    {
                        DtCreated = DateTime.UtcNow,
                        ExpectedDeliveryDate = DateTime.UtcNow,
                        RealDeliveryDate = DateTime.UtcNow,
                        Status = "Active",
                        Total = item.SubTotal,
                        GrandTotal = item.GrandTotal,
                        SupplierId = item.SupplierId,
                        CustomerId = (from c in db.Customers
                                      where c.Id == item.UserId
                                      select c.CustomerId).FirstOrDefault(),
                        ModelId = item.ModelId,
                        CustomerUserId = item.UserId,
                        SupplierUserId = (from s in db.Suppliers
                                          where s.SupplierId == item.SupplierId
                                          select s.Id).FirstOrDefault()
                    };
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                }

                //Just for testing purposes
                string numberOfItems = cartList.Count.ToString();
                return Content("Success {" + numberOfItems + "} Added to Database!!");
            }

            return null;
        }

        public ActionResult ReviewOrder()
        {
            List<CartViewModel> orderList = Session["Order"] as List<CartViewModel>;

            if (orderList != null)
            {
                foreach (var item in orderList)
                {
                    item.ModelName = (from m in db.Models
                                      where m.ModelId == item.ModelId
                                      select m.ModelNumber).FirstOrDefault();

                    item.ImageUrl = (from p in db.Photos
                                     join pm in db.PhotoModels on p.PhotoId equals pm.PhotoId
                                     join m in db.Models on pm.ModelId equals m.ModelId
                                     where m.ModelId == item.ModelId
                                     select p.ImageUrl).FirstOrDefault();

                    item.UnitPrice = (from m in db.Models
                                      where m.ModelId == item.ModelId
                                      select m.Price).FirstOrDefault();

                    item.Quantity = item.Quantity;

                    ViewBag.orderTotal += item.SubTotal;
                }
            }

            return View(orderList);
        }

        [HttpPost]
        public ActionResult ReviewOrder(List<CartViewModel> orders)
        {
            var orderList = Session["Order"] as List<CartViewModel>;
            var orderItems = new List<OrderViewModel>();
            if (orderList != null)
            {
                foreach (var item in orderList)
                {
                    var newOrder = new OrderViewModel()
                    {
                        DtCreated = DateTime.UtcNow,
                        ExpectedDeliveryDate = DateTime.UtcNow,
                        RealDeliveryDate = DateTime.UtcNow,
                        Status = "Active",
                        Total = item.SubTotal,
                        GrandTotal = item.GrandTotal,
                        SupplierId = item.SupplierId,
                        CustomerId = (from c in db.Customers
                                      where c.Id == item.UserId
                                      select c.CustomerId).FirstOrDefault(),
                        ModelId = item.ModelId,
                        CustomerUserId = item.UserId,
                        SupplierUserId = (from s in db.Suppliers
                                          where s.SupplierId == item.SupplierId
                                          select s.Id).FirstOrDefault()
                    };
                    orderItems.Add(newOrder);
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                }
                Session.Remove("Order");
                Session.Remove("Cart");

                //return RedirectToAction("Invoice", "Order", orderItems);
                return View("../Order/Invoice", orderItems);
            }
            return View("../Home/Error404");
        }
    }
}