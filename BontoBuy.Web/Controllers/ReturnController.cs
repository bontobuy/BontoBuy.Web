using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class ReturnController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Return
        public ActionResult Retrieve()
        {
            try
            {
                var records = db.Returns.ToList();
                if (records == null)
                {
                    return RedirectToAction("Home", "Error404");
                }
                return View(records);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Return/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var record = db.Returns.Where(x => x.ReturnId == id);
                if (record == null)
                {
                    return RedirectToAction("Home", "Error404");
                }
                return View();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Return/Create
        public ActionResult Create()
        {
            try
            {
                var newReturn = new ReturnActionViewModel();
                return View(newReturn);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Return/Create
        [HttpPost]
        public ActionResult Create(ReturnActionViewModel item)
        {
            try
            {
                if (item == null)
                    return RedirectToAction("Home", "Error404");

                var newItem = new ReturnViewModel()
                {
                    OrderId = item.OrderId,
                    ReturnDate = item.ReturnDate,
                    ReturnMethod = item.ReturnMethod,
                    Reason = item.Reason,
                    Status = "Processing",
                    DtCreated = DateTime.UtcNow,
                    DtUpdated = DateTime.UtcNow
                };
                db.Returns.Add(newItem);
                db.SaveChanges();

                return RedirectToAction("Retrieve", "Return");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Return/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (id < 1)
                    return RedirectToAction("Home", "Error404");

                var record = db.Returns.Where(x => x.ReturnId == id).FirstOrDefault();

                if (record == null)
                    return RedirectToAction("Home", "Error404");

                var itemToUpdate = new ReturnActionViewModel()
                {
                    ReturnId = record.ReturnId,
                    OrderId = record.OrderId,
                    ReturnDate = record.ReturnDate,
                    ReturnMethod = record.ReturnMethod,
                    Reason = record.Reason,
                    Status = record.Status,
                    DtCreated = record.DtCreated,
                    DtUpdated = record.DtUpdated
                };

                //Refer to Product Controller for View
                ViewBag.ReturnStatusId = new SelectList(db.ReturnStatuses, "ReturnStatusId", "Status");
                return View(itemToUpdate);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Return/Edit/5
        [HttpPost]
        public ActionResult Edit(ReturnActionViewModel item)
        {
            try
            {
                //Refer to Product Controller for View
                ViewBag.ReturnStatusId = new SelectList(db.ReturnStatuses, "ReturnStatusId", "Status", item.ReturnStatusId);
                var itemToUpdate = new ReturnViewModel()
                {
                    ReturnId = item.ReturnId,
                    OrderId = item.OrderId,
                    ReturnDate = item.ReturnDate,
                    ReturnMethod = item.ReturnMethod,
                    Reason = item.Reason,
                    Status = (from rs in db.ReturnStatuses
                              where rs.ReturnStatusId == item.ReturnStatusId
                              select rs.Status).FirstOrDefault(),
                    DtCreated = item.DtCreated,
                    DtUpdated = item.DtUpdated
                };
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Return/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Return/Delete/5
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