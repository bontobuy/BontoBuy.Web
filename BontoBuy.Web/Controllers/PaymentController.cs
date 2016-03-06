using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payment
        public ActionResult Retrieve()
        {
            var records = db.Payments.ToList();
            return View(records);
        }

        // GET: Payment/Details/5
        public ActionResult Details(int id)
        {
            var record = db.Payments.Where(x => x.PaymentId == id);
            return View(record);
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            var newItem = new PaymentViewModel();
            return View(newItem);
        }

        // POST: Payment/Create
        [HttpPost]
        public ActionResult Create(PaymentViewModel item)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payment/Edit/5
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

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payment/Delete/5
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