using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _repository;

        public CategoryController(ICategoryRepo repo)
        {
            _repository = repo;
        }

        // GET: Category
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

        // GET: Category/Details/5
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

        // GET: Category/Create
        public ActionResult Create()
        {
            try
            {
                var newItem = new CategoryViewModel();

                return View(newItem);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                }

                var newItem = _repository.Create(item);

                return RedirectToAction("Retrieve");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Category/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                CategoryViewModel itemToUpdate = new CategoryViewModel(); ;
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

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Update(int id, CategoryViewModel item)
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

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}