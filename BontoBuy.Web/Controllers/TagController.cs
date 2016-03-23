using BontoBuy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class TagController : NotificationController
    {
        private readonly ITagRepo _repository;

        public enum ManageMessageId
        {
            AddSuccess,
            UpdateSuccess,
            ArchiveSuccess,
            RestoreSuccess,
            Error
        }

        public TagController(ITagRepo repo)
        {
            _repository = repo;
        }

        // GET: Tag
        public ActionResult Retrieve(ManageMessageId? message)
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

                    ViewBag.StatusMessage =
                message == ManageMessageId.AddSuccess ? "You have successfully added a new Special Category."
                : message == ManageMessageId.UpdateSuccess ? "You have successfully updated a Special Category."
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

        // GET: Tag/Create
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var newItem = new SpecialCategoryViewModel();

                    var records = _repository.Retrieve();
                    ViewData["TagList"] = records;

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
                        return RedirectToAction("Retrieve", new { message = ManageMessageId.Error });
                    }

                    var newItem = _repository.Create(item);

                    return RedirectToAction("Retrieve", new { message = ManageMessageId.AddSuccess });
                }
                return RedirectToAction("Login", "Account");
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
                        return RedirectToAction("Retrieve", new { message = ManageMessageId.Error });
                    }

                    var updatedItem = _repository.Update(id, item);
                    if (updatedItem == null)
                    {
                        return RedirectToAction("Retrieve", new { message = ManageMessageId.Error });
                    }

                    return RedirectToAction("Retrieve", new { message = ManageMessageId.UpdateSuccess });
                }
                return RedirectToAction("Login", "Account");
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