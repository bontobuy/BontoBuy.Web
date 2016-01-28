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
        private ApplicationDbContext db = new ApplicationDbContext();

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

                //TempData["CategoryData"] = item.CategoryId;
                TempData["CategoryData"] = new ProductViewModel()
                {
                    CategoryId = item.CategoryId
                };

                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: ProductCreation/Details/5
        public ActionResult Details(ProductViewModel item)
        {
            var cat = TempData["CategoryData"];
            return View(cat);
        }

        // GET: ProductCreation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCreation/Create
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
        public ActionResult Delete(int id)
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