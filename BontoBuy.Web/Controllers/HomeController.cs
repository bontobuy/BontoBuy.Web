using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepo _categoryRepository;

        //private readonly IProductRepo _productRepository;
        //private readonly IItemRepo _itemRepository;
        private ApplicationDbContext db = new ApplicationDbContext();

        public HomeController(ICategoryRepo catRepo)
        {
            _categoryRepository = catRepo;

            //_productRepository = prodRepo;
            //_itemRepository = iteRepo;
        }
        public ActionResult Index()
        {
            var categoryRecords = _categoryRepository.Retrieve();
            if (categoryRecords == null)
            {
                return HttpNotFound();
            }

            var productRecords1 = from prod in db.Products
                                  join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                  where cat.CategoryId == 1
                                  where prod.Status == "Active"
                                  select prod;

            if (productRecords1 == null)
            {
                return HttpNotFound();
            }

            var itemRecords1 = from item in db.Items
                               join prod in db.Products on item.ProductId equals prod.ProductId
                               where prod.ProductId == 1
                               where item.Status == "Active"
                               select item;
            if (itemRecords1 == null)
            {
                return HttpNotFound();
            }

            dynamic model = new ExpandoObject();
            model.Category = categoryRecords;
            model.Product1 = productRecords1;
            model.Item1 = itemRecords1;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}