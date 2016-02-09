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
    public class SpecificationController : Controller
    {
        private readonly ISpecificationRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        public SpecificationController(ISpecificationRepo repo)
        {
            _repository = repo;
        }

        // GET: Specification
        public ActionResult Retrieve()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
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

        // GET: Specification/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
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

        // GET: Specification/Create
        public ActionResult Create()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var newItem = new SpecActionViewModel();

                    ViewBag.SpecialCatId = new SelectList(db.SpecialCategories, "SpecialCatId", "Description");
                    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");

                    return View(newItem);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Specification/Create
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

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
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

        // GET: Specification/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    SpecificationViewModel itemToUpdate = new SpecificationViewModel();
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
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Specification/Edit/5
        [HttpPost]
        public ActionResult Update(int id, SpecificationViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    if (item == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Specification cannot be null");
                    }

                    var updatedItem = _repository.Update(id, item);
                    if (updatedItem == null)
                    {
                        return HttpNotFound();
                    }

                    return RedirectToAction("Retrieve");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Specification/Delete/5
        public ActionResult Archive(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
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

        // POST: Specification/Delete/5
        [HttpPost]
        public ActionResult Archive(SpecificationViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var specId = item.SpecificationId;
                    if (specId < 1)
                    {
                        RedirectToAction("Retrieve");
                    }
                    if (item == null)
                    {
                        RedirectToAction("Retrieve");
                    }
                    _repository.Archive(specId);

                    //   return RedirectToAction("Retrieve");
                    return RedirectToAction("Retrieve");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveArchives()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var records = _repository.RetrieveArchives();
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

        public ActionResult RevertArchive(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    if (id < 1)
                    {
                        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product Id cannot be null or empty!");
                        RedirectToAction("RetrieveArchives");
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

        [HttpPost]
        public ActionResult RevertArchive(SpecificationViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var specId = item.SpecificationId;
                    if (specId < 1)
                    {
                        RedirectToAction("RetrieveArchives");
                    }
                    if (item == null)
                    {
                        RedirectToAction("RetrieveArchives");
                    }

                    _repository.RevertArchive(specId);

                    //   return RedirectToAction("Retrieve");
                    return RedirectToAction("RetrieveArchives");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}