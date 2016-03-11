using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Rotativa;

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

                    string supplierEmail = (from s in db.Suppliers
                                            where s.SupplierId == newOrder.SupplierId
                                            select s.Email).FirstOrDefault();

                    string customerEmail = (from c in db.Customers
                                            where c.CustomerId == newOrder.CustomerId
                                            select c.Email).FirstOrDefault();

                    SendNotification(supplierEmail, "Supplier");
                    SendNotification(customerEmail, "Customer");
                }
                return RedirectToAction("CustomerRetrieveOrders", "Customer");
            }

            return RedirectToAction("Login", "Acccount");
        }

        public ActionResult ReviewOrder(int total)
        {
            List<CartViewModel> orderList = Session["Order"] as List<CartViewModel>;
            int grandTotal = total;
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

                    item.GrandTotal = grandTotal;
                }
            }
            ViewData["GrandTotal"] = grandTotal;

            string userId = User.Identity.GetUserId();
            var userAddress = (from d in db.DeliveryAddresses
                               join u in db.Users on d.UserId equals u.Id
                               where d.UserId == userId && d.Status == "Default"
                               select d).FirstOrDefault();
            ViewBag.Street = userAddress.Street;
            ViewBag.City = userAddress.City;
            ViewBag.ZipCode = userAddress.Zipcode;
            Session["DeliveryAddress"] = userAddress;
            return View(orderList);
        }

        [HttpPost]
        public ActionResult ReviewOrder(List<CartViewModel> orders)
        {
            var orderList = Session["Order"] as List<CartViewModel>;
            var deliveryAddress = Session["DeliveryAddress"] as DeliveryAddressViewModel;
            string userId = User.Identity.GetUserId();
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
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Total = item.SubTotal,
                        GrandTotal = item.GrandTotal,
                        SupplierId = item.SupplierId,
                        CustomerId = (from c in db.Customers
                                      where c.Id == userId
                                      select c.CustomerId).FirstOrDefault(),
                        ModelId = item.ModelId,
                        CustomerUserId = userId,

                        SupplierUserId = (from s in db.Suppliers
                                          where s.SupplierId == item.SupplierId
                                          select s.Id).FirstOrDefault()
                    };
                    orderItems.Add(newOrder);
                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    db.Entry(newOrder).GetDatabaseValues();
                    int newOrderId = newOrder.OrderId;

                    var newOrderDelivery = new DeliveryViewModel()
                    {
                        OrderId = newOrderId,
                        Street = deliveryAddress.Street,
                        City = deliveryAddress.City,
                        Zipcode = deliveryAddress.Zipcode,
                        ExpectedDeliveryDate = DateTime.UtcNow,
                        ActualDeliveryDate = DateTime.UtcNow,
                        DateCreated = DateTime.UtcNow,
                        Status = "Processing"
                    };
                    db.Deliveries.Add(newOrderDelivery);
                    db.SaveChanges();

                    GeneratePayment(newOrder.OrderId);
                    string supplierEmail = (from s in db.Suppliers
                                            where s.SupplierId == newOrder.SupplierId
                                            select s.Email).FirstOrDefault();

                    string customerEmail = (from c in db.Customers
                                            where c.CustomerId == newOrder.CustomerId
                                            select c.Email).FirstOrDefault();

                    new Task(() => { SendNotification(supplierEmail, "Supplier"); }).Start();
                    new Task(() => { SendNotification(customerEmail, "Customer"); }).Start();
                }

                Session.Remove("Order");
                Session.Remove("Cart");
                Session.Remove("DeliveryAddress");

                //return RedirectToAction("Invoice", "Order", orderItems);
                //return View("../Order/Invoice", orderItems);
                return RedirectToAction("CustomerRetrieveOrders", "Customer");
            }

            return View("../Home/Error404");
        }

        private void GenerateDelivery(int orderId)
        {
            if (orderId > 1)
            {
                var newDelivery = new DeliveryViewModel()
                {
                    OrderId = orderId,
                };
                db.Deliveries.Add(newDelivery);
                db.SaveChanges();
            }
        }

        private void GeneratePayment(int orderId)
        {
            if (orderId > 1)
            {
                var newPayment = new PaymentViewModel()
                {
                    OrderId = orderId,

                    // CommissionId = 1,
                    DtCreated = DateTime.UtcNow,
                    DtUpdated = DateTime.UtcNow,
                    DiscountAllowed = 10
                };
                db.Payments.Add(newPayment);
                db.SaveChanges();
            }
        }

        private static async void SendNotification(string email, string role)
        {
            if (role == "Supplier")
            {
                string baseUri = "http://localhost:62204/Supplier/SupplierRetrieveOrders";
                var body = "<p>Dear Valued Customer,</p><p>A product has been recently been ordered please take a look<a href=" + baseUri + ">" + baseUri + "</a></p><p>Cheers</p><p>The BontoBuyTeam</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("bontobuy@gmail.com");
                message.Subject = "PDF";
                message.Body = string.Format(body, baseUri);
                message.IsBodyHtml = true;

                var smtp = new SmtpClient();

                var credential = new NetworkCredential()
                {
                    UserName = "bontobuy@gmail.com",
                    Password = "b0nt0@dmin"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            if (role == "Customer")
            {
                string baseUri = "http://localhost:62204/Customer/CustomerRetrieveOrders";
                var body = "<p>Dear Valued Customer,</p><p>Your order is being processed and we will comeback to you shortly. To check you ordere details.Please take a look<a href=" + baseUri + ">" + baseUri + "</a></p><p>Cheers</p><p>The BontoBuyTeam</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("bontobuy@gmail.com");
                message.Subject = "PDF";
                message.Body = string.Format(body, baseUri);
                message.IsBodyHtml = true;

                var smtp = new SmtpClient();

                var credential = new NetworkCredential()
                {
                    UserName = "bontobuy@gmail.com",
                    Password = "b0nt0@dmin"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

        public JsonResult GetDeliveryAddress()
        {
            var userId = User.Identity.GetUserId();
            var addressList = (from d in db.DeliveryAddresses
                               where d.UserId == userId && d.Status == "Optional"
                               select d).ToList();
            return Json(addressList, JsonRequestBehavior.AllowGet);
        }
    }
}