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
    public class SupplierOrderController : Controller
    {
        private ISupplierOrderRepo _repo;

        public SupplierOrderController(ISupplierOrderRepo repository)
        {
            _repo = repository;
        }

        // GET: SupplierOrder
        public ActionResult Retrieve()
        {
            try
            {
                if (User.IsInRole("Supplier"))
                {
                    string userId = User.Identity.GetUserId();
                    if (String.IsNullOrWhiteSpace(userId))
                        return RedirectToAction("Home", "Error404");

                    var records = _repo.Retrieve(userId);
                    if (records == null)
                        return RedirectToAction("Home", "Error404");

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierOrder/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                if (User.IsInRole("Supplier"))
                {
                    string userId = User.Identity.GetUserId();
                    if (String.IsNullOrWhiteSpace(userId))
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Get(id);
                    if (record == null || record.SupplierUserId != userId)
                        return RedirectToAction("Home", "Error404");

                    return View(record);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierOrder/Create
        public ActionResult Create()
        {
            return RedirectToAction("Login", "Account");
        }

        // POST: SupplierOrder/Create
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

        // GET: SupplierOrder/Edit/5
        public ActionResult UpdateOrderStatus(int id)
        {
            try
            {
                if (User.IsInRole("Supplier"))
                {
                    string userId = User.Identity.GetUserId();
                    if (String.IsNullOrWhiteSpace(userId))
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Get(id);
                    if (record == null || record.SupplierUserId != userId)
                        return RedirectToAction("Home", "Error404");

                    return View(record);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: SupplierOrder/Edit/5
        [HttpPost]
        public ActionResult UpdateOrderStatus(OrderViewModel item)
        {
            try
            {
                if (User.IsInRole("Supplier"))
                {
                    string userId = User.Identity.GetUserId();
                    if (String.IsNullOrWhiteSpace(userId))
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.UpdateOrderStatus(item);
                    if (record == null || record.SupplierUserId != userId)
                        return RedirectToAction("Home", "Error404");

                    return View(record);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierOrder/Delete/5
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