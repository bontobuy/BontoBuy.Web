using BontoBuy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class SpecificationController : Controller
    {
        private readonly ISpecificationRepo _repository;

        public SpecificationController(ISpecificationRepo repo)
        {
            _repository = repo;
        }

        // GET: Specification
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

        // GET: Specification/Details/5
        public ActionResult Get(int id)
        {
            try
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
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Specification/Create
        public ActionResult Create()
        {
            var db = new ApplicationDbContext();
            try
            {
                var newItem = new SpecificationViewModel();

                ViewBag.TagId = new SelectList(db.Tags, "TagId", "Description");

                return View(newItem);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Specification/Create
        [HttpPost]
        public ActionResult Create(SpecificationViewModel item)
        {
            var db = new ApplicationDbContext();
            try
            {
                if (item == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                }

                //  var newItem = _repository.Create(item);

                if (ModelState.IsValid)
                {
                    db.Specifications.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Retrieve");
                }
                ViewBag.TagId = new SelectList(db.Tags, "TagId", "Description", item.TagId);
                return View(item);
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

        // POST: Specification/Delete/5
        [HttpPost]
        public ActionResult Archive(SpecificationViewModel item)
        {
            try
            {
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
        public ActionResult RevertArchive(SpecificationViewModel item)
        {
            try
            {
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
            catch
            {
                return View();
            }
        }
    }
}