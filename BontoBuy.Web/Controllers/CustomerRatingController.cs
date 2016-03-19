using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class CustomerRatingController : Controller
    {
        private readonly ICustomerRatingRepo _repo;
        public CustomerRatingController(ICustomerRatingRepo repository)
        {
            _repo = repository;
        }

        // GET: CustomerRating/Get/5
        public ActionResult Get(int id)
        {
            try
            {
                if (User.IsInRole("Customer"))
                {
                    if (id < 1)
                        return RedirectToAction("Error404", "Home");

                    int rating = _repo.GetModelRating(id);
                    return View();
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: CustomerRating/Create
        public ActionResult Create(int id)
        {
            try
            {
                //if (User.IsInRole("Customer"))
                //{
                //The controller needs a modelId to add the rating
                //The customer can only a rating that is from one to five
                int modelId = id;

                var newItem = new RatingModelViewModel()
                {
                    ModelId = modelId
                };
                return View(newItem);

                //}
                //return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: CustomerRating/Create
        [HttpPost]

        // public ActionResult Create(RatingModelViewModel item)
        public ActionResult Create()
        {
            var item = new RatingModelViewModel()
            {
                ModelId = 5,
                RatingId = 4
            };

            try
            {
                if (item == null)
                    return RedirectToAction("Error404", "Home");

                if (User.IsInRole("Customer"))
                {
                    var newItem = _repo.Create(item);
                    if (newItem == null)
                        return RedirectToAction("Error404", "Home");

                    return View(newItem);
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