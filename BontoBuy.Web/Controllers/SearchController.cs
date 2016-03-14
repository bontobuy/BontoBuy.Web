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
            var searchCriteria = Session["SearchCriteria"] as string;
            if (searchCriteria == null)
                return RedirectToAction("Error404", "Home");
            var records = db.Models.Where(x => x.ModelNumber.Contains(searchCriteria)).ToList();

            var searchList = new List<SearchResultViewModel>();
            foreach (var item in records)
            {
                var searchItem = new SearchResultViewModel()
                {
                    ModelId = item.ModelId,
                    ModelName = item.ModelNumber,
                    Price = item.Price,
                    ImageUrl = (from ph in db.Photos
                                join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                join m in db.Models on pm.ModelId equals m.ModelId
                                where pm.ModelId == item.ModelId
                                select ph.ImageUrl).FirstOrDefault(),

                    DtCreated = item.DtCreated,
                    CategoryName = (from c in db.Categories
                                    join p in db.Products on c.CategoryId equals p.CategoryId
                                    join i in db.Items on p.ProductId equals i.ProductId
                                    join m in db.Models on i.ItemId equals m.ItemId
                                    where m.ItemId == item.ItemId
                                    select c.Description).FirstOrDefault(),

                    ProductName = (from c in db.Categories
                                   join p in db.Products on c.CategoryId equals p.CategoryId
                                   join i in db.Items on p.ProductId equals i.ProductId
                                   join m in db.Models on i.ItemId equals m.ItemId
                                   where m.ItemId == item.ItemId
                                   select p.Description).FirstOrDefault(),

                    ItemName = (from c in db.Categories
                                join p in db.Products on c.CategoryId equals p.CategoryId
                                join i in db.Items on p.ProductId equals i.ProductId
                                join m in db.Models on i.ItemId equals m.ItemId
                                where m.ItemId == item.ItemId
                                select i.Description).FirstOrDefault()
                };
                searchList.Add(searchItem);
            }
            int count = records.Count();
            ViewBag.Count = count;
            ViewBag.Model = searchCriteria.ToString();
            return View(searchList);
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

            var records = (from m in db.Models
                           join i in db.Items on m.ItemId equals i.ItemId
                           join p in db.Products on i.ProductId equals p.ProductId
                           join c in db.Categories on p.CategoryId equals c.CategoryId
                           where m.ModelNumber.Contains(filter.ModelName)
                           && m.BrandId == filter.BrandId
                           && c.CategoryId == filter.CategoryId
                           && m.Price >= filter.MinPrice
                           && m.Price <= filter.MaxPrice
                           select m).ToList();

            var searchList = new List<SearchResultViewModel>();
            foreach (var item in records)
            {
                var searchItem = new SearchResultViewModel()
                {
                    ModelId = item.ModelId,
                    ModelName = item.ModelNumber,
                    Price = item.Price,
                    ImageUrl = (from ph in db.Photos
                                join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                join m in db.Models on pm.ModelId equals m.ModelId
                                where pm.ModelId == item.ModelId
                                select ph.ImageUrl).FirstOrDefault(),

                    DtCreated = item.DtCreated,
                    CategoryName = (from c in db.Categories
                                    join p in db.Products on c.CategoryId equals p.CategoryId
                                    join i in db.Items on p.ProductId equals i.ProductId
                                    join m in db.Models on i.ItemId equals m.ItemId
                                    where m.ItemId == item.ItemId
                                    select c.Description).FirstOrDefault(),

                    ProductName = (from c in db.Categories
                                   join p in db.Products on c.CategoryId equals p.CategoryId
                                   join i in db.Items on p.ProductId equals i.ProductId
                                   join m in db.Models on i.ItemId equals m.ItemId
                                   where m.ItemId == item.ItemId
                                   select p.Description).FirstOrDefault(),

                    ItemName = (from c in db.Categories
                                join p in db.Products on c.CategoryId equals p.CategoryId
                                join i in db.Items on p.ProductId equals i.ProductId
                                join m in db.Models on i.ItemId equals m.ItemId
                                where m.ItemId == item.ItemId
                                select i.Description).FirstOrDefault()
                };
                searchList.Add(searchItem);
            }

            int count = records.Count();
            ViewBag.Count = count;
            ViewBag.Model = filter.ModelName;

            //You need to change the View in order to display it
            //You can also put it in a Session and use it elsewhere

            return View("SearchResult", searchList);
        }
    }
}