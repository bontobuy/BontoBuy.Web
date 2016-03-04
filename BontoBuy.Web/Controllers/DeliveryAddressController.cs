using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;

namespace BontoBuy.Web.Controllers
{
    public class DeliveryAddressController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeliveryAddress
        public ActionResult Retrieve()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (User.IsInRole("Admin"))
            {
                var records = db.DeliveryAddresses.ToList();
                var addressList = new List<DeliveryAddressActionViewModel>();
                foreach (var item in records)
                {
                    var addressItem = new DeliveryAddressActionViewModel()
                    {
                        DeliveryAddressId = item.DeliveryAddressId,
                        UserId = item.UserId,
                        CustomerId = item.CustomerId,
                        Street = item.Street,
                        City = item.City,
                        Zipcode = item.Zipcode,
                        Status = item.Status
                    };
                    addressList.Add(addressItem);
                }
                return View(addressList);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: DeliveryAddress/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeliveryAddress/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryAddress/Create
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

        // GET: DeliveryAddress/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeliveryAddress/Edit/5
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

        // GET: DeliveryAddress/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeliveryAddress/Delete/5
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