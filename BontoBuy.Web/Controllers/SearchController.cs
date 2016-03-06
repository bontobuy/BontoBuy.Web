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
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult SearchResult()
        {
            return View();
        }

        public ActionResult AdvancedSearch()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => x.Status == "Active"), "CategoryId", "Description");
            ViewBag.BrandId = new SelectList(db.Brands.Where(b => b.Status == "Active"), "BrandId", "Name");
            return View();
        }
    }
}