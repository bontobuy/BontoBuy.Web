using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ModelController(IModelRepo repo)
        {
            _repository = repo;
        }

        // GET: Model
        public ActionResult Index()
        {
            return View();
        }

        // GET: Model/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Model/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Model/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Model/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Model/Edit/5
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

        // GET: Model/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Model/Delete/5
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

        //// GET: Model/GetModelbyBrand
        //public ActionResult GetModelByBrand()
        //{
        //    try
        //    {
        //        ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");
        //        ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");
        //        ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Description");

        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        //// POST: Model/GetModelbyBrand
        //[HttpPost]
        //public ActionResult GetModelByBrand()
        //{
        //    return View();
        //}
    }
}