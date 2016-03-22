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
    public class SupplierSpecialCategoryController : Controller
    {
        private readonly ITagRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();

        public SupplierSpecialCategoryController(ITagRepo repo)
        {
            _repository = repo;
        }

        // GET: SupplierSpecialCategory
        public ActionResult Retrieve()
        {
            if (User.IsInRole("Supplier"))
            {
                var records = _repository.Retrieve();

                if (records == null)
                {
                    return HttpNotFound();
                }

                return View(records);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: SupplierSpecialCategory/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                if (User.IsInRole("Supplier"))
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
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierSpecialCategory/Create
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole("Supplier"))
                {
                    var newItem = new SpecialCategoryViewModel();

                    var records = _repository.Retrieve();
                    ViewData["TagList"] = records;
                    return View(newItem);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: SupplierSpecialCategory/Create
        [HttpPost]
        public ActionResult Create(SpecialCategoryViewModel item)
        {
            try
            {
                if (User.IsInRole("Supplier"))
                {
                    if (item == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                    }

                    var newItem = _repository.Create(item);

                    return RedirectToAction("Retrieve");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierSpecialCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SupplierSpecialCategory/Edit/5
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

        // GET: SupplierSpecialCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierSpecialCategory/Delete/5
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