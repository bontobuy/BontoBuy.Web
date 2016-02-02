using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepo _repository;

        public BrandController(IBrandRepo repo)
        {
            _repository = repo;
        }

        // GET: Brand
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

        // GET: Brand/Details/5
        public ActionResult Get(int id)
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

        // GET: Brand/Create
        public ActionResult Create()
        {
            try
            {
                var newItem = new BrandViewModel();

                return View(newItem);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(BrandViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Item cannot be null!");
                }

                var newItem = _repository.Create(item);

                return RedirectToAction("Retrieve");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Brand/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                BrandViewModel itemToUpdate = new BrandViewModel();
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

        // POST: Brand/Edit/5
        [HttpPost]
        public ActionResult Update(int id, BrandViewModel item)
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

        // GET: Brand/Delete/5
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

        // POST: Brand/Delete/5
        [HttpPost]
        public ActionResult Archive(BrandViewModel item)
        {
            try
            {
                var brandId = item.BrandId;

                if (brandId < 1 || brandId == null)
                {
                    RedirectToAction("Retrieve");
                }
                if (item == null)
                {
                    RedirectToAction("Retrieve");
                }

                _repository.Archive(brandId);

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
        public ActionResult RevertArchive(BrandViewModel item)
        {
            try
            {
                var brandId = item.BrandId;
                if (brandId < 1 || brandId == null)
                {
                    RedirectToAction("RetrieveArchives");
                }
                if (item == null)
                {
                    RedirectToAction("RetrieveArchives");
                }

                _repository.RevertArchive(brandId);

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