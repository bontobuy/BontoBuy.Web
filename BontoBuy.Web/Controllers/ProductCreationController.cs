using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class ProductCreationController : Controller
    {
        private readonly IProductCreationRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        private IEnumerable<object> records;

        public ProductCreationController(IProductCreationRepo repo)
        {
            _repository = repo;
        }

        // GET: ProductCreation
        public ActionResult CategorySelection()
        {
            var categoryQuery = db.Categories.ToList();
            try
            {
                var newItem = new CategoryViewModel();
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");

                return View(newItem);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: ProductCreation
        [HttpPost]
        public ActionResult CategorySelection(CategoryViewModel item)
        {
            try
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", item.CategoryId);

                TempData["Data"] = new ProductViewModel()
                {
                    ProductId = 0,
                    CategoryId = item.CategoryId,
                    Description = "N/A"
                };

                return RedirectToAction("ProductSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: ProductCreation/5
        public ActionResult ProductSelection()
        {
            try
            {
                ProductViewModel item = TempData["Data"] as ProductViewModel;
                if (item == null)
                {
                    return HttpNotFound();
                }
                records = _repository.RetrieveProductByCategory(item);

                ViewBag.ProductId = new SelectList(records, "ProductId", "Description");

                return View(item);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: ProductSelection
        [HttpPost]
        public ActionResult ProductSelection(ProductViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ProductId = new SelectList(records, "ProductId", "Description", item.ProductId);

                TempData["Data"] = new ItemViewModel()
                {
                    ItemId = 0,
                    ProductId = item.ProductId,
                    Description = "N/A",
                    TermsAndConditions = "N/A"
                };

                return RedirectToAction("ItemSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ItemSelection(ItemViewModel item)
        {
            return Content(item.TermsAndConditions);
        }

        // GET: ProductCreation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductCreation/Edit/5
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

        // GET: ProductCreation/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: ProductCreation/Delete/5
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