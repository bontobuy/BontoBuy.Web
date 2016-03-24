using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class CategoryController : NotificationController
    {
        private readonly ICategoryRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();

        public enum ManageMessageId
        {
            AddCategorySuccess,
            UpdateCategorySuccess,
            ArchiveCategorySuccess,
            RestoreCategorySuccess,
            Error
        }

        public CategoryController(ICategoryRepo repo)
        {
            _repository = repo;
        }

        // GET: Category
        public ActionResult Retrieve(ManageMessageId? message)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                //var role = db.roles.singleordefault(m => m.name == "admin");

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

                    ViewBag.StatusMessage =
                message == ManageMessageId.AddCategorySuccess ? "You have successfully added a new Category."
                : message == ManageMessageId.ArchiveCategorySuccess ? "You have just archive a Category."
                : message == ManageMessageId.UpdateCategorySuccess ? "You have successfully updated a Category."
                : message == ManageMessageId.RestoreCategorySuccess ? "You have successfully restore a Category."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(records);

                    //}

                    //return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Login", "Account");
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

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(profile);
                }
                return RedirectToAction("Login", "Account");
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
                    var newItem = new CategoryViewModel();

                    var records = _repository.Retrieve();

                    ViewData["CategoryList"] = records;

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(newItem);
                }
                return RedirectToAction("Login", "Account");
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
                    var newItem = _repository.Create(item);

                    GetNewSupplierActivation();
                    return RedirectToAction("Retrieve", new { message = ManageMessageId.AddCategorySuccess });
                }
                return RedirectToAction("Login", "Account");
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

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(itemToUpdate);
                }
                return RedirectToAction("Login", "Account");
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
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product cannot be null");
                    }

                    var updatedItem = _repository.Update(id, item);
                    if (updatedItem == null)
                    {
                        return HttpNotFound();
                    }

                    return RedirectToAction("Retrieve", new { message = ManageMessageId.UpdateCategorySuccess });
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Category/Delete/5
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

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
                    return View(profile);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Archive(CategoryViewModel item)
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
                    var categoryId = item.CategoryId;
                    if (categoryId < 1)
                    {
                        RedirectToAction("Retrieve");
                    }
                    if (item == null)
                    {
                        RedirectToAction("Retrieve");
                    }
                    _repository.Archive(categoryId);

                    //   return RedirectToAction("Retrieve");
                    return RedirectToAction("Retrieve", new { message = ManageMessageId.ArchiveCategorySuccess });
                }
                return RedirectToAction("Login", "Account");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult RetrieveArchives(ManageMessageId? message)
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

                    ViewBag.StatusMessage =
                message == ManageMessageId.RestoreCategorySuccess ? "You have successfully restore a Category."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
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

                    GetNewSupplierActivation();
                    GetNewModelsActivation();
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
        public ActionResult RevertArchive(CategoryViewModel item)
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
                    var categoryId = item.CategoryId;
                    if (categoryId < 1)
                    {
                        RedirectToAction("RetrieveArchives");
                    }
                    if (item == null)
                    {
                        RedirectToAction("RetrieveArchives");
                    }

                    _repository.RevertArchive(categoryId);

                    //   return RedirectToAction("Retrieve");
                    return RedirectToAction("RetrieveArchives", new { message = ManageMessageId.RestoreCategorySuccess });
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