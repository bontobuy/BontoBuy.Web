﻿using System;
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

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
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