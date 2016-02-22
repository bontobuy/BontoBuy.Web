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
        private readonly IModelRepo _modelRepository;

        //private readonly IProductRepo _productRepository;
        //private readonly IItemRepo _itemRepository;
        private ApplicationDbContext db = new ApplicationDbContext();

        public HomeController(ICategoryRepo catRepo, IModelRepo modelRepo)
        {
            _categoryRepository = catRepo;
            _modelRepository = modelRepo;

            //_productRepository = prodRepo;
            //_itemRepository = iteRepo;
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult ModelDetails(int id)
        {
            dynamic model = new ExpandoObject();

            if (id < 0)
            {
                return RedirectToAction("Error404", "Home");
            }
            var modelDetails = new HomeCatalogViewModel()
            {
                ModelId = id,
                ModelNumber = db.Models.Find(id).ModelNumber,
                Price = db.Models.Find(id).Price,
                BrandName = (from m in db.Models
                             join b in db.Brands on m.BrandId equals b.BrandId
                             where m.ModelId == id
                             select b.Name).FirstOrDefault(),
                ImageUrl = (from ph in db.Photos
                            join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                            join m in db.Models on pm.ModelId equals m.ModelId
                            where pm.ModelId == id
                            select ph.ImageUrl).FirstOrDefault()
            };
            model.Info = modelDetails;

            var OverviewSpec = (from spec in db.Specifications
                                join specialCat in db.SpecialCategories on spec.SpecialCatId equals specialCat.SpecialCatId
                                where specialCat.Description == "General"
                                select spec).ToList();

            var modelOverviewSpec = new List<ModelFullDetails>();
            foreach (var spec in OverviewSpec)
            {
                var modelSpec = new ModelFullDetails()
                {
                    SpecificationDescription = (from sp in db.Specifications
                                                join modelSpecs in db.ModelSpecs on sp.SpecificationId equals modelSpecs.SpecificationId
                                                where modelSpecs.SpecificationId == spec.SpecificationId && modelSpecs.ModelId == id
                                                select sp.Description).FirstOrDefault(),
                    SpecificationValue = (from s in db.Specifications
                                          join ms in db.ModelSpecs on s.SpecificationId equals ms.SpecificationId
                                          where ms.SpecificationId == spec.SpecificationId && ms.ModelId == id
                                          select ms.Value).FirstOrDefault()
                };
                modelOverviewSpec.Add(modelSpec);
                ViewBag.Overview = (from special in db.SpecialCategories
                                    where special.Description == "Overview"
                                    select special.Description).FirstOrDefault();
            }
            if (modelOverviewSpec == null)
            {
                model.Overview = new EmptyResult();
            }
            model.Overview = modelOverviewSpec;
            return View(model);
        }

        public ActionResult Index()
        {
            dynamic model = new ExpandoObject();

            var categoryRecords = _categoryRepository.Retrieve();
            if (categoryRecords == null)
            {
                return HttpNotFound();
            }
            model.Category = categoryRecords;

            //var productList = new List<ProductViewModel>();

            //foreach (var category in categoryRecords)
            //{
            //    var product = new ProductViewModel()
            //    {
            //        Description = (from p in db.Products
            //                       join c in db.Categories on p.CategoryId equals c.CategoryId
            //                       where p.Status == "Active"
            //                       select p.Description).FirstOrDefault(),
            //        CategoryId = category.CategoryId
            //    };
            //    productList.Add(product);
            //}

            //Products for CategoryId = 1
            var productRecordsC1 = (from prod in db.Products
                                    join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                    where cat.Description == "Mobiles & Tablets"
                                    where prod.Status == "Active"
                                    select prod).ToList();

            if (productRecordsC1 == null)
            {
                return HttpNotFound();
            }
            model.ProductRecordsC1 = productRecordsC1;

            //Items for ProductId = 1
            var itemList1 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 1
                             where i.Status == "Active"
                             select i).ToList();

            model.ItemRecordsP1 = itemList1;

            //Items for ProductId = 2
            var itemList2 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 2
                             where i.Status == "Active"
                             select i).ToList();

            model.ItemRecordsP2 = itemList2;

            //Items for ProductId = 3
            var itemList3 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 3
                             where i.Status == "Active"
                             select i).ToList();

            model.ItemRecordsP3 = itemList3;

            //Items for ProductId = 4
            var itemList4 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 4
                             where i.Status == "Active"
                             select i).ToList();

            model.ItemRecordsP4 = itemList4;

            //Items for ProductId = 5
            var itemList5 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 5
                             where i.Status == "Active"
                             select i).ToList();

            model.ItemRecordsP5 = itemList5;

            //Items for ProductId = 6
            var itemList6 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 6
                             where i.Status == "Active"
                             select i).ToList();

            model.ItemRecordsP6 = itemList6;

            //Items for ProductId = 7
            var itemList7 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 7
                             where i.Status == "Active"
                             select i).ToList();

            model.ItemRecordsP7 = itemList7;

            //Products for CategoryId = 2
            var productRecordsC2 = (from prod in db.Products
                                    join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                    where cat.CategoryId == 2
                                    where prod.Status == "Active"
                                    select prod).ToList();

            if (productRecordsC2 == null)
            {
                return HttpNotFound();
            }
            model.ProductRecordsC2 = productRecordsC2;

            //Items for ProductId = 8
            var itemListP8 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              join c in db.Categories on p.CategoryId equals c.CategoryId
                              where i.ProductId == 8
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP8 = itemListP8;

            //Items for ProductId = 9
            var itemListP9 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              join c in db.Categories on p.CategoryId equals c.CategoryId
                              where i.ProductId == 9
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP9 = itemListP9;

            //Items for ProductId = 10
            var itemList10 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 10
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP10 = itemList10;

            //Items for ProductId = 11
            var itemList11 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 11
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP11 = itemList11;

            //Items for ProductId = 12
            var itemList12 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 12
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP12 = itemList12;

            //Items for ProductId = 13
            var itemList13 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 13
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP13 = itemList13;

            //Items for ProductId = 14
            var itemList14 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 14
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP14 = itemList14;

            //Items for ProductId = 15
            var itemList15 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 15
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP15 = itemList15;

            //Items for ProductId = 16
            var itemList16 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 16
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP16 = itemList16;

            //Items for ProductId = 17
            var itemList17 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 17
                              where i.Status == "Active"
                              select i).ToList();

            model.ItemRecordsP17 = itemList17;

            var modelRecords = _modelRepository.Retrieve();
            var FeaturedList = new List<HomeCatalogViewModel>();
            foreach (var item in modelRecords.Take(3))
            {
                var modelItem = new HomeCatalogViewModel()
                {
                    ModelId = item.ModelId,
                    ModelNumber = item.ModelNumber,
                    Price = item.Price,
                    BrandName = (from m in db.Models
                                 join b in db.Brands on m.BrandId equals b.BrandId
                                 where m.ModelId == item.ModelId
                                 select b.Name).FirstOrDefault(),
                    ImageUrl = (from ph in db.Photos
                                join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                join m in db.Models on pm.ModelId equals m.ModelId
                                where pm.ModelId == item.ModelId
                                select ph.ImageUrl).FirstOrDefault(),
                    DtCreated = (from m in db.Models
                                 where m.ModelId == item.ModelId
                                 select m.DtCreated).FirstOrDefault()
                };
                FeaturedList.Add(modelItem);
                FeaturedList.OrderByDescending(x => x.DtCreated);
            }

            model.FeaturedList = FeaturedList;

            var SlideShowList = new List<HomeCatalogViewModel>();
            foreach (var item in modelRecords)
            {
                var modelImage = new HomeCatalogViewModel()
                {
                    ModelNumber = item.ModelNumber,
                    ImageUrl = (from ph in db.Photos
                                join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                join m in db.Models on pm.ModelId equals m.ModelId
                                where pm.ModelId == item.ModelId
                                select ph.ImageUrl).FirstOrDefault(),
                    ModelId = item.ModelId
                };
                SlideShowList.Add(modelImage);
            }
            model.SlideShow = SlideShowList;

            var NewLaunchesList = new List<HomeCatalogViewModel>();
            foreach (var item in modelRecords.OrderByDescending(x => x.DtCreated).Take(4))
            {
                var modelNewLaunch = new HomeCatalogViewModel()
                {
                    ModelId = item.ModelId,
                    ModelNumber = item.ModelNumber,
                    Price = item.Price,
                    BrandName = (from m in db.Models
                                 join b in db.Brands on m.BrandId equals b.BrandId
                                 where m.ModelId == item.ModelId
                                 select b.Name).FirstOrDefault(),
                    ImageUrl = (from ph in db.Photos
                                join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                join m in db.Models on pm.ModelId equals m.ModelId
                                where pm.ModelId == item.ModelId
                                select ph.ImageUrl).FirstOrDefault(),

                    DtCreated = (from m in db.Models
                                 where m.ModelId == item.ModelId
                                 select m.DtCreated).FirstOrDefault()
                };
                NewLaunchesList.Add(modelNewLaunch);
            }

            model.NewLaunchList = NewLaunchesList;

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

        public JsonResult GetNewModels()
        {
            try
            {
                var ModelRecords = _modelRepository.Retrieve().OrderByDescending(i => i.DtCreated).Take(4);
                var NewModelList = new List<HomeCatalogViewModel>().ToList();
                foreach (var item in ModelRecords)
                {
                    var NewModel = new HomeCatalogViewModel()
                    {
                        ModelId = item.ModelId,
                        ModelNumber = item.ModelNumber,
                        Price = item.Price,
                        BrandName = (from m in db.Models
                                     join b in db.Brands on m.BrandId equals b.BrandId
                                     where m.ModelId == item.ModelId
                                     select b.Name).FirstOrDefault(),
                        ImageUrl = (from ph in db.Photos
                                    join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                    join m in db.Models on pm.ModelId equals m.ModelId
                                    where pm.ModelId == item.ModelId
                                    select ph.ImageUrl).FirstOrDefault(),

                        DtCreated = (from m in db.Models
                                     where m.ModelId == item.ModelId
                                     select m.DtCreated).FirstOrDefault()
                    };
                    NewModelList.Add(NewModel);
                }
                return Json(NewModelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }
    }
}