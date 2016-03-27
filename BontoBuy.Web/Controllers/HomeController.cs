using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using PagedList;
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
            if (User.IsInRole("Supplier"))
                return RedirectToAction("Index", "Supplier");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

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
                            select ph.ImageUrl).FirstOrDefault(),

                Supplier = (from m in db.Models
                            join u in db.Users on m.UserId equals u.Id
                            where m.ModelId == id
                            select u.Name).FirstOrDefault()
            };
            model.Info = modelDetails;

            var modelSpecRecords = (from ms in db.ModelSpecs
                                    where ms.ModelId == id
                                    select ms).ToList();

            var modelSpecList = new List<ModelSpecificationDetails>();

            foreach (var item in modelSpecRecords)
            {
                var modelSpecification = new ModelSpecificationDetails()
                {
                    SpecificationName = (from sp in db.Specifications
                                         join modelSpecs in db.ModelSpecs on sp.SpecificationId equals modelSpecs.SpecificationId
                                         where modelSpecs.SpecificationId == item.SpecificationId && modelSpecs.ModelId == id
                                         select sp.Description).FirstOrDefault(),
                    SpecificationValue = item.Value,
                    SpecialCategoryName = (from sp in db.SpecialCategories
                                           join s in db.Specifications on sp.SpecialCatId equals s.SpecialCatId
                                           where s.SpecificationId == item.SpecificationId
                                           select sp.Description).FirstOrDefault(),

                    SpecialCategoryPosition = (from sp in db.SpecialCategories
                                               join s in db.Specifications on sp.SpecialCatId equals s.SpecialCatId
                                               where s.SpecificationId == item.SpecificationId
                                               select sp.Position).FirstOrDefault()
                };
                modelSpecList.Add(modelSpecification);
            }

            model.SpecificationDetails = modelSpecList.GroupBy(m => m.SpecialCategoryName).ToDictionary(g => g.Key, g => g.ToList());

            int modelItemId = (from m in db.Models
                               where m.ModelId == id
                               select m.ItemId).FirstOrDefault();
            var relatedRecords = (from m in db.Models
                                  where m.ItemId == modelItemId && m.Status == "Active"
                                  select m).ToList();
            var relatedList = new List<HomeCatalogViewModel>();
            foreach (var item in relatedRecords.OrderBy(f => Guid.NewGuid()).Take(15))
            {
                var relatedItem = new HomeCatalogViewModel()
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
                relatedList.Add(relatedItem);
            }
            model.RelatedProducts = relatedList;

            return View(model);
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Supplier"))
                return RedirectToAction("Index", "Supplier");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

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
                             where i.AdminStatus == "Active"
                             select i).ToList();

            model.ItemRecordsP1 = itemList1;

            //Items for ProductId = 2
            var itemList2 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 2
                             where i.AdminStatus == "Active"
                             select i).ToList();

            model.ItemRecordsP2 = itemList2;

            //Items for ProductId = 3
            var itemList3 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 3
                             where i.AdminStatus == "Active"
                             select i).ToList();

            model.ItemRecordsP3 = itemList3;

            //Items for ProductId = 4
            var itemList4 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 4
                             where i.AdminStatus == "Active"
                             select i).ToList();

            model.ItemRecordsP4 = itemList4;

            //Items for ProductId = 5
            var itemList5 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 5
                             where i.AdminStatus == "Active"
                             select i).ToList();

            model.ItemRecordsP5 = itemList5;

            //Items for ProductId = 6
            var itemList6 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 6
                             where i.AdminStatus == "Active"
                             select i).ToList();

            model.ItemRecordsP6 = itemList6;

            //Items for ProductId = 7
            var itemList7 = (from i in db.Items
                             join p in db.Products on i.ProductId equals p.ProductId
                             where i.ProductId == 7
                             where i.AdminStatus == "Active"
                             select i).ToList();

            model.ItemRecordsP7 = itemList7;

            //Products for CategoryId = 2
            var productRecordsC2 = (from prod in db.Products
                                    join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                    where cat.Description == "Computers"
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
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP8 = itemListP8;

            //Items for ProductId = 9
            var itemListP9 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              join c in db.Categories on p.CategoryId equals c.CategoryId
                              where i.ProductId == 9
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP9 = itemListP9;

            //Items for ProductId = 10
            var itemList10 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 10
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP10 = itemList10;

            //Items for ProductId = 11
            var itemList11 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 11
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP11 = itemList11;

            //Items for ProductId = 12
            var itemList12 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 12
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP12 = itemList12;

            //Items for ProductId = 13
            var itemList13 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 13
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP13 = itemList13;

            //Items for ProductId = 14
            var itemList14 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 14
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP14 = itemList14;

            //Items for ProductId = 15
            var itemList15 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 15
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP15 = itemList15;

            //Items for ProductId = 16
            var itemList16 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 16
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP16 = itemList16;

            //Items for ProductId = 17
            var itemList17 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 17
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP17 = itemList17;

            //Products for CategoryId = 3
            var productRecordsC3 = (from prod in db.Products
                                    join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                    where cat.Description == "Gaming"
                                    where prod.Status == "Active"
                                    select prod).ToList();

            if (productRecordsC3 == null)
            {
                return HttpNotFound();
            }
            model.ProductRecordsC3 = productRecordsC3;

            //Items for ProductId = 18
            var itemListP18 = (from i in db.Items
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where i.ProductId == 18
                               where i.AdminStatus == "Active"
                               select i).ToList();

            model.ItemRecordsP18 = itemListP18;

            //Products for CategoryId = 4
            var productRecordsC4 = (from prod in db.Products
                                    join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                    where cat.Description == "Electronics"
                                    where prod.Status == "Active"
                                    select prod).ToList();

            if (productRecordsC4 == null)
            {
                return HttpNotFound();
            }
            model.ProductRecordsC4 = productRecordsC4;

            //Items for ProductId = 19
            var itemListP19 = (from i in db.Items
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where i.ProductId == 19
                               where i.AdminStatus == "Active"
                               select i).ToList();

            model.ItemRecordsP19 = itemListP19;

            //Items for ProductId = 20
            var itemListP20 = (from i in db.Items
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where i.ProductId == 20
                               where i.AdminStatus == "Active"
                               select i).ToList();

            model.ItemRecordsP20 = itemListP20;

            //Items for ProductId = 21
            var itemList21 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 21
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP21 = itemList21;

            //Items for ProductId = 22
            var itemList22 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 22
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP22 = itemList22;

            //Items for ProductId = 23
            var itemList23 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 23
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP23 = itemList23;

            //Items for ProductId = 24
            var itemList24 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 24
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP24 = itemList24;

            //Items for ProductId = 25
            var itemList25 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 25
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP25 = itemList25;

            //Products for CategoryId = 5
            var productRecordsC5 = (from prod in db.Products
                                    join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                    where cat.Description == "Women's Fashion"
                                    where prod.Status == "Active"
                                    select prod).ToList();

            if (productRecordsC5 == null)
            {
                return HttpNotFound();
            }
            model.ProductRecordsC5 = productRecordsC5;

            //Items for ProductId = 26
            var itemListP26 = (from i in db.Items
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where i.ProductId == 26
                               where i.AdminStatus == "Active"
                               select i).ToList();

            model.ItemRecordsP26 = itemListP26;

            //Items for ProductId = 27
            var itemListP27 = (from i in db.Items
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where i.ProductId == 27
                               where i.AdminStatus == "Active"
                               select i).ToList();

            model.ItemRecordsP27 = itemListP27;

            //Items for ProductId = 28
            var itemList28 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 28
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP28 = itemList28;

            //Items for ProductId = 29
            var itemList29 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 29
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP29 = itemList29;

            //Items for ProductId = 30
            var itemList30 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 30
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP30 = itemList30;

            //Items for ProductId = 31
            var itemList31 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 31
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP31 = itemList31;

            //Items for ProductId = 32
            var itemList32 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 32
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP32 = itemList32;

            //Items for ProductId = 33
            var itemList33 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 33
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP33 = itemList33;

            //Items for ProductId = 34
            var itemList34 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 34
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP34 = itemList34;

            //Products for CategoryId = 6
            var productRecordsC6 = (from prod in db.Products
                                    join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                    where cat.Description == "Men's Fashion"
                                    where prod.Status == "Active"
                                    select prod).ToList();

            if (productRecordsC6 == null)
            {
                return HttpNotFound();
            }
            model.ProductRecordsC6 = productRecordsC6;

            //Items for ProductId = 35
            var itemListP35 = (from i in db.Items
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where i.ProductId == 35
                               where i.AdminStatus == "Active"
                               select i).ToList();

            model.ItemRecordsP35 = itemListP35;

            //Items for ProductId = 36
            var itemListP36 = (from i in db.Items
                               join p in db.Products on i.ProductId equals p.ProductId
                               join c in db.Categories on p.CategoryId equals c.CategoryId
                               where i.ProductId == 36
                               where i.AdminStatus == "Active"
                               select i).ToList();

            model.ItemRecordsP36 = itemListP36;

            //Items for ProductId = 37
            var itemList37 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 37
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP37 = itemList37;

            //Items for ProductId = 38
            var itemList38 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 38
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP38 = itemList38;

            //Items for ProductId = 39
            var itemList39 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 39
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP39 = itemList39;

            //Items for ProductId = 40
            var itemList40 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 40
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP40 = itemList40;

            //Items for ProductId = 41
            var itemList41 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 41
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP41 = itemList41;

            //Items for ProductId = 42
            var itemList42 = (from i in db.Items
                              join p in db.Products on i.ProductId equals p.ProductId
                              where i.ProductId == 42
                              where i.AdminStatus == "Active"
                              select i).ToList();

            model.ItemRecordsP42 = itemList42;

            var MobileCategoryRecords = (from m in db.Models
                                         join i in db.Items on m.ItemId equals i.ItemId
                                         join p in db.Products on i.ProductId equals p.ProductId
                                         join c in db.Categories on p.CategoryId equals c.CategoryId
                                         where c.Description == "Mobiles & Tablets" && i.AdminStatus == "Active"
                                         select m).ToList();

            var MobileCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in MobileCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var MobileCategoryData = new HomeCatalogViewModel()
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
                MobileCategoryDisplay = MobileCategoryData;
            }
            model.MobileCategory = MobileCategoryDisplay;

            var ComputerCategoryRecords = (from m in db.Models
                                           join i in db.Items on m.ItemId equals i.ItemId
                                           join p in db.Products on i.ProductId equals p.ProductId
                                           join c in db.Categories on p.CategoryId equals c.CategoryId
                                           where c.Description == "Computers" && i.AdminStatus == "Active"
                                           select m).ToList();

            var ComputerCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in ComputerCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var ComputerCategoryData = new HomeCatalogViewModel()
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
                ComputerCategoryDisplay = ComputerCategoryData;
            }
            model.ComputerCategory = ComputerCategoryDisplay;

            var GamingCategoryRecords = (from m in db.Models
                                         join i in db.Items on m.ItemId equals i.ItemId
                                         join p in db.Products on i.ProductId equals p.ProductId
                                         join c in db.Categories on p.CategoryId equals c.CategoryId
                                         where c.Description == "Gaming" && i.AdminStatus == "Active"
                                         select m).ToList();

            var GamingCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in GamingCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var GamingCategoryData = new HomeCatalogViewModel()
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
                GamingCategoryDisplay = GamingCategoryData;
            }
            model.GamingCategory = GamingCategoryDisplay;

            var ElectronicsCategoryRecords = (from m in db.Models
                                              join i in db.Items on m.ItemId equals i.ItemId
                                              join p in db.Products on i.ProductId equals p.ProductId
                                              join c in db.Categories on p.CategoryId equals c.CategoryId
                                              where c.Description == "Electronics" && i.AdminStatus == "Active"
                                              select m).ToList();

            var ElectronicsCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in ElectronicsCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var ElectronicsCategoryData = new HomeCatalogViewModel()
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
                ElectronicsCategoryDisplay = ElectronicsCategoryData;
            }
            model.ElectronicsCategory = ElectronicsCategoryDisplay;

            var WomenCategoryRecords = (from m in db.Models
                                        join i in db.Items on m.ItemId equals i.ItemId
                                        join p in db.Products on i.ProductId equals p.ProductId
                                        join c in db.Categories on p.CategoryId equals c.CategoryId
                                        where c.Description == "Women's Fashion" && i.AdminStatus == "Active"
                                        select m).ToList();

            var WomenCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in WomenCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var CategoryData = new HomeCatalogViewModel()
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
                WomenCategoryDisplay = CategoryData;
            }
            model.WomenCategory = WomenCategoryDisplay;

            var MenCategoryRecords = (from m in db.Models
                                      join i in db.Items on m.ItemId equals i.ItemId
                                      join p in db.Products on i.ProductId equals p.ProductId
                                      join c in db.Categories on p.CategoryId equals c.CategoryId
                                      where c.Description == "Men's Fashion" && i.AdminStatus == "Active"
                                      select m).ToList();

            var MenCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in MenCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var CategoryData = new HomeCatalogViewModel()
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
                MenCategoryDisplay = CategoryData;
            }
            model.MenCategory = MenCategoryDisplay;

            var KidsCategoryRecords = (from m in db.Models
                                       join i in db.Items on m.ItemId equals i.ItemId
                                       join p in db.Products on i.ProductId equals p.ProductId
                                       join c in db.Categories on p.CategoryId equals c.CategoryId
                                       where c.Description == "Kids' Fashion" && i.AdminStatus == "Active"
                                       select m).ToList();

            var KidsCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in KidsCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var CategoryData = new HomeCatalogViewModel()
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
                KidsCategoryDisplay = CategoryData;
            }
            model.KidsCategory = KidsCategoryDisplay;

            var HomeCategoryRecords = (from m in db.Models
                                       join i in db.Items on m.ItemId equals i.ItemId
                                       join p in db.Products on i.ProductId equals p.ProductId
                                       join c in db.Categories on p.CategoryId equals c.CategoryId
                                       where c.Description == "Home & Kitchen" && i.AdminStatus == "Active"
                                       select m).ToList();

            var HomeCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in HomeCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var CategoryData = new HomeCatalogViewModel()
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
                HomeCategoryDisplay = CategoryData;
            }
            model.HomeCategory = HomeCategoryDisplay;

            var SportCategoryRecords = (from m in db.Models
                                        join i in db.Items on m.ItemId equals i.ItemId
                                        join p in db.Products on i.ProductId equals p.ProductId
                                        join c in db.Categories on p.CategoryId equals c.CategoryId
                                        where c.Description == "Sport, Fitness & Outdoor" && i.AdminStatus == "Active"
                                        select m).ToList();

            var SportCategoryDisplay = new HomeCatalogViewModel();
            foreach (var item in SportCategoryRecords.OrderBy(m => Guid.NewGuid()).Take(1))
            {
                var CategoryData = new HomeCatalogViewModel()
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
                SportCategoryDisplay = CategoryData;
            }
            model.SportCategory = SportCategoryDisplay;

            //Products for CategoryId = Financial
            var productRecordsC10 = (from prod in db.Products
                                     join cat in db.Categories on prod.CategoryId equals cat.CategoryId
                                     where cat.Description == "Financial Services"
                                     where prod.Status == "Active"
                                     select prod).ToList();

            if (productRecordsC10 == null)
            {
                return View("Index");
            }
            model.ProductRecordsC10 = productRecordsC10;

            //Items for ProductName = Financial Services
            var itemListPFinance = (from i in db.Items
                                    join p in db.Products on i.ProductId equals p.ProductId
                                    join c in db.Categories on p.CategoryId equals c.CategoryId
                                    where p.Description == "Insurance"
                                    where i.AdminStatus == "Active"
                                    select i).ToList();

            model.ItemRecordsPFinance = itemListPFinance;

            var modelRecords = _modelRepository.Retrieve();
            var FeaturedList = new List<HomeCatalogViewModel>();
            foreach (var item in modelRecords.Where(x => x.Status == "Active").OrderBy(f => Guid.NewGuid()).Take(3))
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
            Random randomNumber = new Random();
            foreach (var item in modelRecords.Where(x => x.Status == "Active").OrderBy(s => randomNumber.Next(1, 20)))
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
            foreach (var item in modelRecords.Where(x => x.Status == "Active").OrderByDescending(x => x.DtCreated).Take(4))
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

            var NewLaunchesList2 = new List<HomeCatalogViewModel>();
            foreach (var item in modelRecords.OrderByDescending(x => Guid.NewGuid()).Take(4))
            {
                if (item.Status == "Active")
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
                    NewLaunchesList2.Add(modelNewLaunch);
                }
            }

            model.NewLaunchList2 = NewLaunchesList2;

            return View(model);
        }

        [HttpPost]
        public JsonResult AddCartItem(int id)
        {
            try
            {
                var cartList = new List<CartViewModel>();
                List<CartViewModel> updatedCartList = Session["Cart"] as List<CartViewModel>;
                if (updatedCartList == null)
                {
                    var cartItem = new CartViewModel()
                    {
                        ModelId = id,
                        ModelName = (from m in db.Models
                                     where m.ModelId == id
                                     select m.ModelNumber).FirstOrDefault(),
                        ImageUrl = (from p in db.Photos
                                    join pm in db.PhotoModels on p.PhotoId equals pm.PhotoId
                                    join m in db.Models on pm.ModelId equals m.ModelId
                                    where m.ModelId == id
                                    select p.ImageUrl).FirstOrDefault(),

                        UnitPrice = (from m in db.Models
                                     where m.ModelId == id
                                     select m.Price).FirstOrDefault()
                    };
                    cartList.Add(cartItem);
                    Session["Cart"] = cartList;
                }
                if (updatedCartList != null)
                {
                    var cartItem = new CartViewModel()
                    {
                        ModelId = id,
                        ModelName = (from m in db.Models
                                     where m.ModelId == id
                                     select m.ModelNumber).FirstOrDefault(),
                        ImageUrl = (from p in db.Photos
                                    join pm in db.PhotoModels on p.PhotoId equals pm.PhotoId
                                    join m in db.Models on pm.ModelId equals m.ModelId
                                    where m.ModelId == id
                                    select p.ImageUrl).FirstOrDefault(),

                        UnitPrice = (from m in db.Models
                                     where m.ModelId == id
                                     select m.Price).FirstOrDefault()
                    };
                    updatedCartList.Add(cartItem);
                    Session.Remove("Cart");
                    Session["Cart"] = updatedCartList;
                }

                List<CartViewModel> Cart = Session["Cart"] as List<CartViewModel>;
                var carCount = Cart.Count();

                //var productList = db.Products.Where(p => p.CategoryId == id).ToList();
                return Json(new { Cart, carCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
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

        [HttpPost]
        public ActionResult Index(string searchCriteria)
        {
            if (User.IsInRole("Supplier"))
                return RedirectToAction("Index", "Supplier");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            if (String.IsNullOrEmpty(searchCriteria))
            {
                return RedirectToAction("Index", "Home");
            }
            Session["SearchCriteria"] = searchCriteria;
            return RedirectToAction("SearchResult", "Search");
        }

        public ActionResult Catalog(int id, int? page)
        {
            if (User.IsInRole("Supplier"))
                return RedirectToAction("Index", "Supplier");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            if (id < 0)
            {
                return RedirectToAction("Error404", "Home");
            }
            var records = (from m in db.Models
                           where m.ItemId == id && m.Status == "Active"
                           select m).ToList();

            var CatalogList = new List<HomeCatalogViewModel>();
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");
            foreach (var item in records)
            {
                var CatalogItem = new HomeCatalogViewModel()
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
                CatalogList.Add(CatalogItem);
            }
            Session["ItemId"] = id.ToString();

            //Paging Section
            var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
            var pageOfProducts = CatalogList.ToPagedList(pageNumber, 10); //set the number of records per page
            ViewBag.pageOfProducts = pageOfProducts;

            return View();
        }

        public ActionResult CatalogByBrand()
        {
            if (User.IsInRole("Supplier"))
                return RedirectToAction("Index", "Supplier");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            var ItemId = Session["ItemId"] as string;
            return RedirectToAction("Catalog", new { id = ItemId });
        }

        [HttpPost]
        public ActionResult CatalogByBrand(int BrandId, int? page)
        {
            if (User.IsInRole("Supplier"))
                return RedirectToAction("Index", "Supplier");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            string ItemIdInString = Session["ItemId"] as string;
            int ItemId = Convert.ToInt32(ItemIdInString);
            var brandTest = BrandId;

            var records = db.Models.Where(m => m.ItemId == ItemId && m.BrandId == BrandId && m.Status == "Active").ToList();

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");

            var CatalogList = new List<HomeCatalogViewModel>();
            foreach (var item in records)
            {
                var CatalogItem = new HomeCatalogViewModel()
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
                CatalogList.Add(CatalogItem);
            }

            var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
            var pageOfProducts = CatalogList.ToPagedList(pageNumber, 10); //set the number of records per page
            ViewBag.pageOfProducts = pageOfProducts;

            return View("Catalog");
        }

        [HttpPost]
        public ActionResult CatalogByPrice(int minPrice, int maxPrice, int? page)
        {
            if (User.IsInRole("Supplier"))
                return RedirectToAction("Index", "Supplier");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            string ItemIdInString = Session["ItemId"] as string;
            int ItemId = Convert.ToInt32(ItemIdInString);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");

            var records = db.Models.Where(m => m.Price >= minPrice && m.Price <= maxPrice && m.Status == "Active" && m.ItemId == ItemId).ToList();
            var CatalogList = new List<HomeCatalogViewModel>();
            foreach (var item in records)
            {
                var CatalogItem = new HomeCatalogViewModel()
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
                CatalogList.Add(CatalogItem);
            }

            var pageNumber = page ?? 1; // if no pagenumber is specified in the querystring, it will assign pageNumber to 1 by default
            var pageOfProducts = CatalogList.ToPagedList(pageNumber, 10); //set the number of records per page
            ViewBag.pageOfProducts = pageOfProducts;

            return View("Catalog");
        }
    }
}