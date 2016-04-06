using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class SupplierCommissionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupplierCommission
        public ActionResult Retrieve()
        {
            if (User.IsInRole("Admin"))
            {
                var records = db.Suppliers.Where(x => x.Status == "Active").ToList();
                return View(records);
            }

            return RedirectToAction("Login", "Account");
        }

        // GET: SupplierCommission/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SupplierCommission/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierCommission/Create
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

        // GET: SupplierCommission/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var recordToUpdate = db.Suppliers.Where(x => x.Id == id).FirstOrDefault();
                    if (recordToUpdate.SupplierId < 1)
                        return RedirectToAction("Error404", "Home");

                    return View(recordToUpdate);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: SupplierCommission/Edit/5
        [HttpPost]
        public ActionResult Edit(SupplierViewModel item)
        {
            try
            {
                //if (User.IsInRole("Admin"))
                //{
                //}

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: SupplierCommission/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierCommission/Delete/5
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