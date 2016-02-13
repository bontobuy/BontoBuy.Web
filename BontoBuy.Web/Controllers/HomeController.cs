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

            //var productList = new List<ProductViewModel>();

            //foreach (var category in categoryRecords)
            //{
            //    var product = new ProductViewModel()
            //    {
            //        Description = (from p in db.Products
            //                       join c in db.Categories on p.CategoryId equals c.CategoryId
            //                       where p.CategoryId == category.CategoryId
            //                       select p.Description).FirstOrDefault()
            //    };
            //    productList.Add(product);
            //}

            //var productRecords = from prod in db.Products
            //                     join cat in db.Categories on prod.CategoryId equals cat.CategoryId
            //                     where cat.CategoryId == 1
            //                     where prod.Status == "Active"
            //                     select prod;

            //if (productRecords == null)
            //{
            //    return HttpNotFound();
            //}

            //var itemRecords1 = from item in db.Items
            //                   join prod in db.Products on item.ProductId equals prod.ProductId
            //                   where prod.ProductId == 1
            //                   where item.Status == "Active"
            //                   select item;
            //if (itemRecords1 == null)
            //{
            //    return HttpNotFound();
            //}

            dynamic model = new ExpandoObject();
            model.Category = categoryRecords;

            //model.Product = productRecords;
            //model.Item1 = itemRecords1;

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

        [HttpPost]
        public JsonResult GetProductsForCategory(int id)
        {
            try
            {
                var productList = db.Products.Where(p => p.CategoryId == id).ToList();
                return Json(productList);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetItemsForProduct(int id)
        {
            try
            {
                var ItemList = db.Items.Where(i => i.ProductId == id).ToList();
                return Json(ItemList);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }
    }
}