using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class CommissionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Commission
        public ActionResult Retrieve()
        {
            var records = db.Commissions.ToList();
            return View();
        }

        // GET: Commission/Details/5
        public ActionResult Details(int id)
        {
            var record = db.Commissions.Where(x => x.CommissionId == id);
            return View(record);
        }

        // GET: Commission/Create
        public ActionResult Create()
        {
            //var newItem = new CommissionViewModel();
            var newItem = new CommissionViewModel()
            {
                Percentage = 5,
                DtCreated = DateTime.UtcNow,
                DtUpdated = DateTime.UtcNow
            };

            return View(newItem);
        }

        // POST: Commission/Create
        [HttpPost]
        public ActionResult Create(CommissionViewModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (item.Percentage < 0)
                    {
                        return RedirectToAction("Home", "Error404");
                    }
                    db.Commissions.Add(item);
                    db.SaveChanges();
                }

                return RedirectToAction("Retrieve");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Commission/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Commission/Edit/5
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

        // GET: Commission/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Commission/Delete/5
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