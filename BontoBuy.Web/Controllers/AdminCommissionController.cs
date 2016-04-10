using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class AdminCommissionController : Controller
    {
        private IAdminCommissionRepo _repo;

        public AdminCommissionController(IAdminCommissionRepo repository)
        {
            _repo = repository;
        }

        // GET: AdminCommission
        public ActionResult Retrieve()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.Retrieve();
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

        // GET: AdminCommission/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id < 1)
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Get(id);
                    if (record == null)
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

        // GET: AdminCommission/Create
        public ActionResult Create()
        {
            return RedirectToAction("Home", "Error404");
        }

        // POST: AdminCommission/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            return RedirectToAction("Home", "Error404");
        }

        // GET: AdminCommission/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id < 1)
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Get(id);
                    if (record == null)
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

        // POST: AdminCommission/Edit/5
        [HttpPost]
        public ActionResult Update(CommissionViewModel item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (item == null)
                        return RedirectToAction("Home", "Error404");

                    var record = _repo.Update(item);
                    if (record == null)
                        return RedirectToAction("Home", "Error404");

                    return RedirectToAction("Retrieve");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveDeliveredOrders()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrieveDeliveredOrders();
                    if (records == null)
                        return RedirectToAction("Error404", "Home");

                    return View(records);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrievePaidOrders()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.RetrievePaidOrders();

                    if (records == null)
                        return RedirectToAction("Error404", "Home");

                    return View(records);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult UpdateCommissionOwnedFromOrders(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var record = _repo.UpdateCommissionOwnedFromOrders(id);

                    if (record == null)
                        return RedirectToAction("Error404", "Home");

                    return View(record);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}