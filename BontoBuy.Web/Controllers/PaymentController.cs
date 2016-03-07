using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;

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
            return RedirectToAction("Home", "Error404");
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
            var userId = User.Identity.GetUserId();
            if (userId == null || id < 1)
            {
                return RedirectToAction("Home", "Error404");
            }
            var record = (from p in db.Payments
                          join o in db.Orders on p.OrderId equals o.OrderId
                          where o.OrderId == id
                          && o.SupplierUserId == userId
                          select p).FirstOrDefault();

            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatuses, "PaymentStatusId", "Status");

            if (record == null)
            {
                return RedirectToAction("Home", "Error404");
            }

            var itemToUpdate = new PaymentActionViewModel()
            {
                PaymentId = record.PaymentId,
                OrderId = record.OrderId,
                CommissionId = record.CommissionId,
                DtCreated = record.DtCreated,
                DtUpdated = DateTime.UtcNow,
                DiscountAllowed = record.DiscountAllowed,
                Status = record.Status
            };

            return View(itemToUpdate);
        }

        // POST: Payment/Edit/5
        [HttpPost]
        public ActionResult Edit(PaymentActionViewModel item)
        {
            try
            {
                if (item == null || item.PaymentId == null)
                {
                    return RedirectToAction("Home", "Error404");
                }
                ViewBag.PaymentStatusId = new SelectList(db.PaymentStatuses, "PaymentStatusId", "Status", item.PaymentStatusId);

                var itemToUpdate = db.Payments.Where(x => x.PaymentId == item.PaymentId).FirstOrDefault();

                itemToUpdate.Status = (from ps in db.PaymentStatuses
                                       where ps.PaymentStatusId == item.PaymentStatusId
                                       select ps.Status).FirstOrDefault();

                db.SaveChanges();

                return RedirectToAction("Retrieve", "PaymentSupplier");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Home", "Error404");
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