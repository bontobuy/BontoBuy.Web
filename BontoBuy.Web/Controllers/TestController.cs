using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test
        public ActionResult Index()
        {
            //var records = from r in db.Categories
            //              select new { r.CategoryId, r.Description };

            //ViewBag.myList = records
            //    .Select(x => new SelectListItem
            //    {
            //        Text = x.Description,
            //        Value = x.CategoryId.ToString()
            //    }).ToList();

            //List<SelectListItem> categoryListItems = new List<SelectListItem>();
            //foreach (CategoryViewModel category in db.Categories)
            //{
            //    SelectListItem catListItem = new SelectListItem
            //    {
            //        Text = category.Description,
            //        Value = category.CategoryId.ToString(),
            //        Selected = category.IsSelected.HasValue ? category.IsSelected.Value : false
            //    };
            //    categoryListItems.Add(catListItem);
            //}
            //ViewBag.Categories = categoryListItems;

            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            //return View();

            using (ApplicationDbContext myDb = new ApplicationDbContext())
            {
                var records = from modelSpecs in db.ModelSpecs
                              join specs in db.Specifications on modelSpecs.SpecificationId equals specs.SpecificationId
                              join tags in db.Tags on specs.TagId equals tags.TagId
                              where tags.TagId == 1
                              select modelSpecs;
                if (records != null)
                {
                    foreach (var item in records)
                    {
                        item.Description = (from specs in db.Specifications
                                            join modelSpecs in db.ModelSpecs on specs.SpecificationId equals modelSpecs.SpecificationId
                                            where specs.SpecificationId.Equals(item.SpecificationId)
                                            select specs.Description).FirstOrDefault();

                        return View(records.ToList());
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(List<ModelSpecViewModel> records)
        {
            foreach (ModelSpecViewModel item in records)
            {
                ModelSpecViewModel newItem = db.ModelSpecs.Find(item.SpecificationId);
                newItem.ModelId = item.ModelId;
                newItem.Value = item.Value;
                newItem.Description = item.Description;
            }
            db.SaveChanges();

            return View();
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
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

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
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

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
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