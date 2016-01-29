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

                TempData["Data"] = new ProductViewModel()
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
                ProductViewModel item = TempData["Data"] as ProductViewModel;
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

                TempData["Data"] = new ItemViewModel()
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
                ItemViewModel item = TempData["Data"] as ItemViewModel;
                if (item == null)
                {
                    return HttpNotFound();
                }
                records = _repository.RetrieveItemByProduct(item);

                ViewBag.Item = new SelectList(records, "ItemId", "Description");

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

                var assignItem = new ModelViewModel();
                ViewBag.ItemId = new SelectList(records, "ProductId", "Description", assignItem.ItemId);

                TempData["Data"] = assignItem;

                return RedirectToAction("BrandSelection");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult BrandSelection()
        {
            return View();

            //try
            //{
            //    ItemViewModel item = TempData["Data"] as ItemViewModel;
            //    if (item == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    records = _repository.RetrieveItemByProduct(item);

            //    ViewBag.Item = new SelectList(records, "ItemId", "Description");

            //    return View(item);
            //}
            //catch (Exception ex)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            //}
        }

        public ActionResult ModelSelection()
        {
            try
            {
                ModelViewModel item = TempData["Data"] as ModelViewModel;
                if (item == null)
                {
                    return HttpNotFound();
                }

                records = _repository.RetrieveModelByItemByBrand(item);
                ViewBag.Model = new SelectList(records, "ModelId", "Description");

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
                ViewBag.Model = new SelectList(records, "ModelId", "Description", item.ModelId);

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
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}