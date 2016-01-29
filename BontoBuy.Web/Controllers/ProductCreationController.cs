using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.Controllers
{
    public class ProductCreationController : Controller
    {
        private readonly IProductCreationRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        private IEnumerable<object> records;

        public ProductCreationController(IProductCreationRepo repo)
        {
            _repository = repo;
        }

        // GET: ProductCreation
        public ActionResult CategorySelection()
        {
            var categoryQuery = db.Categories.ToList();
            try
            {
                var newItem = new CategoryViewModel();
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");

                return View(newItem);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: ProductCreation
        [HttpPost]
        public ActionResult CategorySelection(CategoryViewModel item)
        {
            try
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", item.CategoryId);

                TempData["CategoryData"] = new ProductViewModel()
                {
                    ProductId = 0,
                    CategoryId = item.CategoryId,
                    Description = ""
                };

                return RedirectToAction("ProductSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: ProductCreation/5
        public ActionResult ProductSelection()
        {
            try
            {
                ProductViewModel item = TempData["CategoryData"] as ProductViewModel;
                if (item == null)
                {
                    return HttpNotFound();
                }
                records = _repository.RetrieveProductByCategory(item);
                ViewBag.ProductId = new SelectList(records, "ProductId", "Description");

                return View(item);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: ProductSelection
        [HttpPost]
        public ActionResult ProductSelection(ProductViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return HttpNotFound();
                }

                records = _repository.RetrieveProductByCategory(item);
                ViewBag.ProductId = new SelectList(records, "ProductId", "Description", item.ProductId);

                TempData["ProductData"] = new ItemViewModel()
                {
                    ItemId = 0,
                    ProductId = item.ProductId,
                    Description = ""
                };

                return RedirectToAction("ItemSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ItemSelection()
        {
            try
            {
                ItemViewModel item = TempData["ProductData"] as ItemViewModel;
                if (item == null)
                {
                    return HttpNotFound();
                }
                records = _repository.RetrieveItemByProduct(item);

                ViewBag.ItemId = new SelectList(records, "ItemId", "Description");

                return View(item);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ItemSelection(ItemViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return HttpNotFound();
                }

                records = _repository.RetrieveItemByProduct(item);

                ViewBag.ItemId = new SelectList(records, "ItemId", "Description", item.ItemId);

                TempData["ModelData"] = new ModelViewModel()
                {
                    ItemId = item.ItemId
                };

                return RedirectToAction("BrandSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult BrandSelection()
        {
            try
            {
                var item = new BrandViewModel();

                records = _repository.RetrieveBrand();

                ViewBag.BrandId = new SelectList(records, "BrandId", "Name");

                return View(item);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult BrandSelection(BrandViewModel item)
        {
            try
            {
                records = _repository.RetrieveBrand();
                var itemData = TempData["ModelData"] as ModelViewModel;
                ViewBag.BrandId = new SelectList(records, "BrandId", "Name", item.BrandId);

                TempData["ModelData"] = new ModelViewModel()
                {
                    ModelId = 0,
                    ItemId = itemData.ItemId,
                    BrandId = item.BrandId,
                    ModelNumber = "",
                    Price = -1
                };

                return RedirectToAction("ModelSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ModelSelection()
        {
            try
            {
                ModelViewModel item = TempData["ModelData"] as ModelViewModel;
                if (item == null)
                {
                    return HttpNotFound();
                }

                records = _repository.RetrieveModelByItemByBrand(item);
                ViewBag.ModelId = new SelectList(records, "ModelId", "ModelNumber");

                return View(item);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ModelSelection(ModelViewModel item)
        {
            try
            {
                if (item == null)
                {
                    return HttpNotFound();
                }

                records = _repository.RetrieveModelByItemByBrand(item);
                ViewBag.ModelId = new SelectList(records, "ModelId", "ModelNumber", item.ModelId);

                TempData["Data"] = new ModelSpecViewModel()
                {
                    ModelId = item.ModelId,
                    SpecificationId = 0,
                    Value = ""
                };

                return RedirectToAction("ModelSpecSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ModelSpecSelection()
        {
            try
            {
                return Content("Success");
            }
            catch
            {
                return View();
            }
        }
    }
}