using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo _repository;

        public ProductController(IProductRepo repo)
        {
            _repository = repo;
        }

        // GET: Product
        public ActionResult Retrieve()
        {
            try
            {
                var records = _repository.Retrieve();

                if (records == null)
                {
                    return HttpNotFound();
                }

                return View(records);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Product/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                if (id < 1)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product Id cannot be null or empty!");
                    RedirectToAction("Retrieve");
                }

                var profile = _repository.Get(id);
                if (profile == null)
                {
                    return HttpNotFound();
                }

                return View(profile);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var db = new ApplicationDbContext();
            try
            {
                var newItem = new ProductViewModel();

                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");

                return View(newItem);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel item)
        {
            var db = new ApplicationDbContext();
            try
            {
                if (item == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                }

                //  var newItem = _repository.Create(item);

                if (ModelState.IsValid)
                {
                    db.Products.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Retrieve");
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", item.CategoryId);
                return View(item);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Product/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                ProductViewModel itemToUpdate = new ProductViewModel();
                if (id < 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Identifier");
                }

                itemToUpdate = _repository.Get(id);
                if (itemToUpdate == null)
                {
                    return HttpNotFound();
                }

                return View(itemToUpdate);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Update(int id, ProductViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product cannot be null");
                }

                var updatedItem = _repository.Update(id, item);
                if (updatedItem == null)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Retrieve");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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