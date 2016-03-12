using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class CustomerWishlistController : Controller
    {
        // GET: CustomerWishlist
        public ActionResult Retrieve()
        {
            return View();
        }

        // GET: CustomerWishlist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerWishlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerWishlist/Create
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

        // GET: CustomerWishlist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerWishlist/Edit/5
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

        // GET: CustomerWishlist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerWishlist/Delete/5
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