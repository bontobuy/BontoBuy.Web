using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;

namespace BontoBuy.Web.Controllers
{
    public class CustomerPaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerPayment
        public ActionResult Retrieve()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Home", "Error404");
            }
            var records = from p in db.Payments
                          join o in db.Orders on p.OrderId equals o.OrderId
                          where o.CustomerUserId == userId
                          select o;

            return View(records);
        }

        // GET: CustomerPayment/Details/5
        public ActionResult Get(int id)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null || id < 1)
            {
                return RedirectToAction("Home", "Error404");
            }
            var record = (from p in db.Payments
                          join o in db.Orders on p.OrderId equals o.OrderId
                          where o.OrderId == id
                          && o.CustomerUserId == userId
                          select o).FirstOrDefault();
            return View(record);
        }

        // GET: CustomerPayment/Create
        public ActionResult Create()
        {
            return RedirectToAction("Home", "Error404");
        }

        // POST: CustomerPayment/Create
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

        // GET: CustomerPayment/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Home", "Error404");
        }

        // POST: CustomerPayment/Edit/5
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

        // GET: CustomerPayment/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Home", "Error404");
        }

        // POST: CustomerPayment/Delete/5
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