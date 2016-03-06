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
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult SearchResult()
        {
            var searchCriteria = Session["SearchCriteria"] as string;
            if (searchCriteria == null)
                return RedirectToAction("Home", "Error404");

            var records = db.Models.Where(x => x.ModelNumber.Contains(searchCriteria)).ToList();

            return View(records);
        }

        public ActionResult AdvancedSearch()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => x.Status == "Active"), "CategoryId", "Description");
            ViewBag.BrandId = new SelectList(db.Brands.Where(b => b.Status == "Active"), "BrandId", "Name");
            var searchCriteria = new SearchFilter();

            return View(searchCriteria);
        }

        [HttpPost]
        public ActionResult AdvancedSearch(SearchFilter filter)
        {
            if (filter == null)
            {
                return RedirectToAction("Home", "Error404");
            }
            if (filter.MaxPrice < 1 && filter.MinPrice < 1)
            {
                filter.MaxPrice = 1000000;
                filter.MinPrice = 1;
            }
            if (filter.MaxPrice < 1)
            {
                filter.MaxPrice = 1000000;
            }
            if (filter.MinPrice < 1)
            {
                filter.MinPrice = 1;
            }

            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => x.Status == "Active"), "CategoryId", "Description", filter.CategoryId);
            ViewBag.BrandId = new SelectList(db.Brands.Where(b => b.Status == "Active"), "BrandId", "Name", filter.BrandId);

            var searchResult = from m in db.Models
                               join i in db.Items on m.ItemId equals i.ItemId
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where m.BrandId == filter.BrandId
                               && c.CategoryId == filter.CategoryId
                               select m;

            //You need to change the View in order to display it
            //You can also put it in a Session and use it elsewhere

            return View(searchResult);
        }
    }
}