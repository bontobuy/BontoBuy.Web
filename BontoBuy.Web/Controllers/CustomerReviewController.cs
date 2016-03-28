using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class CustomerReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
                    string modelName = (from m in db.Models
                                        where m.ModelId == id
                                        select m.ModelNumber).FirstOrDefault();
                    ViewBag.ModelName = modelName;
                    bool elegible = false;
                    string userId = User.Identity.GetUserId();

                    if (modelId < 1 || String.IsNullOrWhiteSpace(userId))
                        return RedirectToAction("Error404", "Home");

                    elegible = _repo.VerifyReviewAbility(userId, id);
                    if (elegible == false)
                        return RedirectToAction("CustomerRetrieveOrders", "Customer", new { message = ManageMessageId.ReviewFailure });

                    var review = new ReviewViewModel()
                    {
                        UserId = userId,
                        ModelId = modelId
                    };
                    Session["Review"] = review;
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
        public ActionResult Create(ReviewViewModel item, int rating)
        {
            try
            {
                var reviewDetails = Session["Review"] as ReviewViewModel;
                var test = rating;
                if (User.IsInRole("Customer"))
                {
                    if (item == null)
                        return RedirectToAction("Error404", "Home");

                    var newRecord = new ReviewViewModel
                    {
                        Description = item.Description,
                        UserId = reviewDetails.UserId,
                        ModelId = reviewDetails.ModelId,
                        DtCreated = DateTime.UtcNow
                    };
                    db.Reviews.Add(newRecord);
                    db.SaveChanges();

                    int ratingId = (from r in db.Ratings
                                    where r.RatingNumber == rating
                                    select r.RatingId).FirstOrDefault();
                    var newRating = new RatingModelViewModel()
                    {
                        ModelId = reviewDetails.ModelId,
                        RatingId = ratingId
                    };
                    db.RatingModels.Add(newRating);
                    db.SaveChanges();
                    Session.Remove("Review");
                    return RedirectToAction("CustomerRetrieveOrders", "Customer", new { message = ManageMessageId.ReviewSuccess });
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public enum ManageMessageId
        {
            ReviewSuccess,
            ReviewFailure,
            Error
        }
    }
}