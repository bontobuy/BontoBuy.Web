using BontoBuy.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class ReturnController : NotificationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public enum ManageMessageId
        {
            UpdateSuccess,
            Error
        }

        // GET: Return
        public ActionResult Retrieve(ManageMessageId? message, int? page)
        {
            try
            {
                var records = db.Returns.ToList();
                if (records == null)
                {
                    return RedirectToAction("Index", "Admin", new { message = ManageMessageId.Error });
                }

                ViewBag.StatusMessage =
                message == ManageMessageId.UpdateSuccess ? "You have successfully updated a Item."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                //Paging Section
                var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
                var pageOfProducts = records.ToPagedList(pageNumber, 10); //set the number of records per page
                ViewBag.pageOfProducts = pageOfProducts;

                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Return/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                var record = db.Returns.Where(x => x.ReturnId == id).FirstOrDefault();
                if (record == null)
                {
                    return RedirectToAction("Home", "Error404");
                }

                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View(record);
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

                GetNewSupplierActivation();
                GetNewModelsActivation();
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
                    return RedirectToAction("Retrieve", "Return", new { message = ManageMessageId.Error });

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

                GetNewSupplierActivation();
                GetNewModelsActivation();
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
                if (item.ReturnStatusId == 1)
                {
                    itemToUpdate.HasApproved = true;
                }
                else { itemToUpdate.HasApproved = false; }
                db.SaveChanges();
                return RedirectToAction("Retrieve", "Return", new { message = ManageMessageId.UpdateSuccess });
            }
            catch
            {
                return RedirectToAction("Retrieve", "Return", new { message = ManageMessageId.Error });
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