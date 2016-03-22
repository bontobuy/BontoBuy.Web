using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;

namespace BontoBuy.Web.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult RetrieveModels()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (String.IsNullOrEmpty(userId))
                    return RedirectToAction("Error404", "Home");

                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();

                if (User.IsInRole("Supplier") && user.ActivationCode == null)
                {
                    var getSupplier = (from s in db.Suppliers
                                       where s.Id == userId
                                       select s).FirstOrDefault();

                    var retrieveModels = (from m in db.Models
                                          join ms in db.ModelSpecs on m.ModelId equals ms.ModelId
                                          join s in db.Suppliers on ms.SupplierId equals s.SupplierId

                                          select m).Distinct();

                    var modelList = new List<SupplierRetrieveModelsViewModel>();

                    foreach (var obj in retrieveModels)
                    {
                        var model = new SupplierRetrieveModelsViewModel()
                        {
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
                                        select ph.ImageUrl).FirstOrDefault()
                        };

                        modelList.Add(model);
                    }

                    return View(modelList);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (String.IsNullOrEmpty(userId))
                    return RedirectToAction("Error404", "Home");

                int modelId = id;
                if (modelId < 1)
                    return RedirectToAction("Error404", "Home");

                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();

                if (User.IsInRole("Customer") && user.ActivationCode == null)
                {
                    var getSupplier = (from s in db.Suppliers
                                       where s.Id == userId
                                       select s).FirstOrDefault();

                    var modelSpecList = (from ms in db.ModelSpecs
                                         where ms.ModelId == modelId
                                         select ms).ToList();

                    var modelSpecVM = new List<SupplierGetModelSpecViewModel>();

                    foreach (var obj in modelSpecList)
                    {
                        var model = new SupplierGetModelSpecViewModel()
                        {
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

                    var cartData = new ModelToCartViewModel()
                    {
                        ModelId = modelId,
                        SupplierId = (from s in db.Suppliers
                                      join m in db.Models on s.SupplierId equals m.SupplierId
                                      where m.ModelId == modelId
                                      select s.SupplierId).FirstOrDefault()
                    };

                    Session["cartData"] = cartData;

                    return View(modelSpecVM);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult Details(List<SupplierGetModelSpecViewModel> items)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (String.IsNullOrEmpty(userId))
                    return RedirectToAction("Error404", "Home");

                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (User.IsInRole("Customer") && user.ActivationCode == null)
                {
                    var cartData = Session["cartData"] as ModelToCartViewModel;
                    var cartList = new List<CartViewModel>();
                    List<CartViewModel> updatedCartList = Session["Cart"] as List<CartViewModel>;
                    if (updatedCartList == null)
                    {
                        var cartItem = new CartViewModel()
                        {
                            ModelId = cartData.ModelId,
                            SupplierId = cartData.SupplierId
                        };
                        cartList.Add(cartItem);
                        Session["Cart"] = cartList;
                    }
                    if (updatedCartList != null)
                    {
                        var cartItem = new CartViewModel()
                        {
                            ModelId = cartData.ModelId,
                            SupplierId = cartData.SupplierId
                        };
                        updatedCartList.Add(cartItem);
                        Session.Remove("Cart");
                        Session["Cart"] = updatedCartList;
                    }
                    return RedirectToAction("NavigateToCart");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult NavigateToCart()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (String.IsNullOrEmpty(userId))
                    return RedirectToAction("Error404", "Home");

                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (User.IsInRole("Customer") && user.ActivationCode == null)
                {
                    List<CartViewModel> cartList = Session["Cart"] as List<CartViewModel>;

                    if (cartList != null)
                    {
                        foreach (var item in cartList)
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

                            //item.Quantity = 1;
                            //item.SubTotal = (item.Quantity * item.UnitPrice);
                        }
                        return View(cartList);
                    }
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult NavigateToCart(FormCollection collection)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (String.IsNullOrEmpty(userId))
                    return RedirectToAction("Error404", "Home");

                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (User.IsInRole("Customer") && user.ActivationCode == null)
                {
                    //var quantity = order.Quantity;
                    List<CartViewModel> orderList = Session["Cart"] as List<CartViewModel>;

                    //string userId = User.Identity.GetUserId();
                    //if (userId == null)
                    //{
                    //    string returnUrl = Request.Url.ToString();
                    //    return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
                    //}
                    //else { userId = User.Identity.GetUserId(); }
                    int quantity = 0;
                    int total = 0;
                    var quantityArray = collection.GetValues("item.Quantity");
                    foreach (var item in orderList)
                    {
                        //item.ModelName = (from m in db.Models
                        //                  where m.ModelId == item.ModelId
                        //                  select m.ModelNumber).FirstOrDefault();
                        //item.ImageUrl = (from p in db.Photos
                        //                 join pm in db.PhotoModels on p.PhotoId equals pm.PhotoId
                        //                 join m in db.Models on pm.ModelId equals m.ModelId
                        //                 where m.ModelId == item.ModelId
                        //                 select p.ImageUrl).FirstOrDefault();
                        item.UnitPrice = (from m in db.Models
                                          where m.ModelId == item.ModelId
                                          select m.Price).FirstOrDefault();
                        item.Quantity = Convert.ToInt32(quantityArray[quantity]);
                        item.SubTotal = (item.Quantity * item.UnitPrice);

                        //item.GrandTotal += item.SubTotal;
                        total += item.SubTotal;

                        //item.UserId = userId;
                        item.SupplierId = db.Models.Find(item.ModelId).SupplierId;

                        //item.Quantity = 1;
                        quantity++;
                    }
                    Session["Order"] = orderList;

                    return RedirectToAction("ReviewOrder", "Order", new { total = total });
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public JsonResult RemoveCartItem(int id)
        {
            List<CartViewModel> cartList = Session["Cart"] as List<CartViewModel>;

            var itemToRemove = cartList.FindIndex(m => m.ModelId == id);
            cartList.RemoveAt(itemToRemove);

            Session["Cart"] = cartList;

            int count = cartList.Count();
            return Json(new { count, cartList }, JsonRequestBehavior.AllowGet);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}