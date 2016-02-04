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
    public class ItemController : Controller
    {
        private readonly IItemRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ItemController(IItemRepo repo)
        {
            _repository = repo;
        }

        // GET: Item
        public ActionResult Retrieve()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        var records = _repository.Retrieve();
                        if (records == null)
                        {
                            return HttpNotFound();
                        }
                        return View(records);
                    }

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Item/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        if (id < 1)
                        {
                            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item Id cannot be null or empty!");
                            RedirectToAction("Retrieve");
                        }

                        var profile = _repository.Get(id);
                        if (profile == null)
                        {
                            return HttpNotFound();
                        }

                        return View(profile);
                    }

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        var newItem = new ItemViewModel();
                        ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");

                        return View(newItem);
                    }

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(ItemViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        var newItem = new ItemViewModel();
                        var newTag = new TagViewModel();
                        if (ModelState.IsValid)
                        {
                            db.Items.Add(item);
                            db.SaveChanges();

                            //We are adding a Tag matching the description of the Item
                            newTag.Description = item.Description;
                            db.Tags.Add(newTag);
                            db.SaveChanges();

                            return RedirectToAction("Retrieve");
                        }
                        ViewBag.ProductId = new SelectList(db.Products.Where(x => x.Status == "Active"), "ProductId", "Description", item.ProductId);

                        return View(newItem);
                    }

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Item/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        ItemViewModel itemToUpdate = new ItemViewModel();
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

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Update(int id, ItemViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
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

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Item/Delete/5
        public ActionResult Archive(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
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

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Archive(ItemViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        var itemId = item.ItemId;
                        if (itemId < 1)
                        {
                            RedirectToAction("Retrieve");
                        }
                        if (item == null)
                        {
                            RedirectToAction("Retrieve");
                        }
                        _repository.Archive(itemId);

                        //   return RedirectToAction("Retrieve");
                        return RedirectToAction("Retrieve");
                    }

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
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
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        var records = _repository.RetrieveArchives();
                        if (records == null)
                        {
                            return HttpNotFound();
                        }
                        return View(records);
                    }

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
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
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
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

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult RevertArchive(ItemViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginAdmin", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (role != null)
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    if (userInRole != null)
                    {
                        var itemId = item.ItemId;
                        if (itemId < 1)
                        {
                            RedirectToAction("RetrieveArchives");
                        }
                        if (item == null)
                        {
                            RedirectToAction("RetrieveArchives");
                        }

                        _repository.RevertArchive(itemId);

                        //   return RedirectToAction("Retrieve");
                        return RedirectToAction("RetrieveArchives");
                    }

                    return RedirectToAction("LoginAdmin", "Account");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}