using BontoBuy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepo _repository;

        public TagController(ITagRepo repo)
        {
            _repository = repo;
        }

        // GET: Tag
        public ActionResult Retrieve()
        {
            try
            {
                if (User.IsInRole("Admin"))
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
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Tag/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
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
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var newItem = new SpecialCategoryViewModel();

                    return View(newItem);
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create(SpecialCategoryViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (item == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                    }

                    var newItem = _repository.Create(item);

                    return RedirectToAction("Retrieve");
                }
                return RedirectToAction("LoginAdmin", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Tag/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    SpecialCategoryViewModel itemToUpdate = new SpecialCategoryViewModel();
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
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Update(int id, SpecialCategoryViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
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
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tag/Delete/5
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