using BontoBuy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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

        // GET: Item/Details/5
        public ActionResult Get(int id)
        {
            try
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
                var newItem = new ItemViewModel();
                ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");

                return View(newItem);
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
                ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", item.ProductId);

                return View(newItem);
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

        // GET: Item/Delete/5
        public ActionResult Archive(int id)
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

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Archive(ItemViewModel item)
        {
            try
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
            catch
            {
                return View();
            }
        }

        public ActionResult RetrieveArchives()
        {
            try
            {
                var records = _repository.RetrieveArchives();

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

        public ActionResult RevertArchive(int id)
        {
            try
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
            catch
            {
                return View();
            }
        }
    }
}