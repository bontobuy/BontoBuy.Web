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
    public class CustomerReviewController : Controller
    {
        private readonly ICustomerReviewRepo _repo;

        public CustomerReviewController(ICustomerReviewRepo repository)
        {
            _repo = repository;
        }

        // GET: CustomerReview/Retrieve/5
        public ActionResult Retrieve(int id)
        {
            try
            {
                if (User.IsInRole("Customer"))
                {
                    if (id < 1)
                        return RedirectToAction("Error404", "Home");

                    var records = _repo.Retrieve(id);
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

        // GET: CustomerReview/Create
        public ActionResult Create(int id)
        {
            try
            {
                if (User.IsInRole("Customer"))
                {
                    int modelId = id;
                    bool elegible = false;
                    string userId = User.Identity.GetUserId();

                    if (modelId < 1 || String.IsNullOrWhiteSpace(userId))
                        return RedirectToAction("Error404", "Home");

                    elegible = _repo.VerifyReviewAbility(userId, id);
                    if (elegible == false)
                        return RedirectToAction("Retrieve", "CustomerReview");

                    var review = new ReviewViewModel()
                    {
                        UserId = userId,
                        ModelId = modelId
                    };

                    return View(review);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: CustomerReview/Create
        [HttpPost]
        public ActionResult Create(ReviewViewModel item)
        {
            try
            {
                if (User.IsInRole("Customer"))
                {
                    var newItem = _repo.Create(item);

                    if (item == null)
                        return RedirectToAction("Error404", "Home");

                    return RedirectToAction("Retrieve", "CustomerReview");
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