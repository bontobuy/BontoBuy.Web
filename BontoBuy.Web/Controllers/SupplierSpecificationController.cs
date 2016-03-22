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
    public class SupplierSpecificationController : Controller
    {
        private readonly ISpecificationRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();

        public SupplierSpecificationController(ISpecificationRepo repo)
        {
            _repository = repo;
        }

        // GET: SupplierSpecification
        public ActionResult Retrieve()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

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
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierSpecification/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    if (id < 1)
                    {
                        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Specification Id cannot be null or empty!");
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

        // GET: SupplierSpecification/Create
        public ActionResult Create()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    var newItem = new SpecActionViewModel();

                    ViewBag.SpecialCatId = new SelectList(db.SpecialCategories, "SpecialCatId", "Description");
                    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");

                    var records = _repository.Retrieve();
                    ViewData["SpecificationList"] = records;
                    return View(newItem);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: SupplierSpecification/Create
        [HttpPost]
        public ActionResult Create(SpecActionViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    if (item == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                    }

                    //  var newItem = _repository.Create(item);
                    var spec = new SpecificationViewModel()
                    {
                        SpecialCatId = item.SpecialCatId,
                        Status = "Active",
                        Description = item.Description
                    };

                    var productSpec = new ProductSpecViewModel()
                    {
                        ProductId = item.ProductId,
                        SpecificationId = -1
                    };

                    if (ModelState.IsValid)
                    {
                        db.Specifications.Add(spec);
                        db.SaveChanges();
                        productSpec.SpecificationId = spec.SpecificationId;
                        db.ProductSpecs.Add(productSpec);
                        db.SaveChanges();
                        return RedirectToAction("Retrieve");
                    }
                    ViewBag.TagId = new SelectList(db.SpecialCategories, "SpecialCatId", "Description", spec.SpecialCatId);
                    return View(item);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierSpecification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SupplierSpecification/Edit/5
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

        // GET: SupplierSpecification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierSpecification/Delete/5
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