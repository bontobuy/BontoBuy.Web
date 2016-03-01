using System;
using System.Collections.Generic;
using System.Linq;
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
            string userId = User.Identity.GetUserId();

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

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            int modelId = id;
            string userId = User.Identity.GetUserId();
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

        [HttpPost]
        public ActionResult Details(List<SupplierGetModelSpecViewModel> items)
        {
            var cartData = Session["cartData"] as ModelToCartViewModel;
            string userId = User.Identity.GetUserId();
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

        public ActionResult NavigateToCart()
        {
            string userId = User.Identity.GetUserId();

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
            }

            return View(cartList);
        }

        [HttpPost]
        public ActionResult NavigateToCart(CartViewModel order)
        {
            //var quantity = order.Quantity;
            List<CartViewModel> orderList = Session["Cart"] as List<CartViewModel>;

            //var quantity = ViewData["Quantity"].ToString();
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

                item.Quantity = order.Quantity;

                item.SubTotal = order.SubTotal;

                item.GrandTotal += order.SubTotal;

                //item.Quantity = Convert.ToInt32(quantity);

                //item.SubTotal = (item.Quantity * item.UnitPrice);
            }

            Session["Order"] = orderList;
            return RedirectToAction("ReviewOrder", "Order");
        }

        public JsonResult GetModelDetails(int quantity)
        {
            var quantityValue = quantity;
            ViewData["Quantity"] = quantityValue;
            return Json(quantityValue);
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