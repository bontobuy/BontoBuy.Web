using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class AdminItemController : Controller
    {
        private readonly IAdminItemRepo _repo;
        public AdminItemController(IAdminItemRepo repository)
        {
            _repo = repository;
        }

        // GET: AdminItem
        public ActionResult Retrieve()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.Retrieve();
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminItem/Get/5
        public ActionResult Get(int id)
        {
            try
            {
                if (id < 1)
                    return RedirectToAction("Home", "Error404");

                if (User.IsInRole("Admin"))
                {
                    var record = _repo.Get(id);
                    if (record == null)
                        return RedirectToAction("Home", "Error404");

                    return View(record);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminItem/Create
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var newItem = new AdminCreateItemViewModel();
                    var products = _repo.AdminRetrieveProducts();
                    if (products == null)
                        return RedirectToAction("Home", "Error404");

                    ViewBag.ProductId = new SelectList(products, "ProductId", "Description");

                    return View(newItem);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: AdminItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminCreateItemViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item == null)
                            return RedirectToAction("Home", "Error404");

                        var newItem = _repo.Create(item);
                        if (newItem == null)
                            return RedirectToAction("Home", "Error404");

                        return RedirectToAction("AdminItem", "Retrieve");
                    }
                    return RedirectToAction("Home", "Error404");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminItem/Update/5
        public ActionResult Update(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id < 1)
                        return RedirectToAction("Home", "Error404");

                    var itemToUpdate = _repo.GetUpdateItemViewModel(id);
                    if (itemToUpdate == null || itemToUpdate.ItemId < 1)
                        return RedirectToAction("Home", "Error404");

                    return View(itemToUpdate);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: AdminItem/Update/5
        [HttpPost]
        public ActionResult Update(AdminUpdateItemViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (item == null || item.ItemId < 1)
                        return null;

                    var itemToUpdate = _repo.Update(item);
                    if (itemToUpdate == null || itemToUpdate.ItemId < 1)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("AdminItem", "Retrieve");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminItem
        public ActionResult RetrieveArchives()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveArchives();
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminItem/Archive/5
        public ActionResult Archive(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    bool archive = _repo.Archive(id);
                    if (archive == false)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("AdminItem", "Retrieve");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: AdminItem/Archive/5
        public ActionResult RevertArchive(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    bool revertArchive = _repo.RevertArchive(id);
                    if (revertArchive == false)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("AdminItem", "Retrieve");
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