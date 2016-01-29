using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ModelController(IModelRepo repo)
        {
            _repository = repo;
        }

        // GET: Model
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

        // GET: Model/Details/5
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

        // GET: Model/Create
        public ActionResult Create()
        {
            try
            {
                var newItem = new ModelViewModel();

                ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");
                ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Description");

                return View(newItem);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Model/Create
        [HttpPost]
        public ActionResult Create(ModelViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                }

                // var newItem = _repository.Create(item);
                if (ModelState.IsValid)
                {
                    db.Models.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Retrieve");
                }

                return RedirectToAction("Retrieve");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Model/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                ModelViewModel itemToUpdate = new ModelViewModel();
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

        // POST: Model/Edit/5
        [HttpPost]
        public ActionResult Update(int id, ModelViewModel item)
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

        // GET: Model/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Model/Delete/5
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