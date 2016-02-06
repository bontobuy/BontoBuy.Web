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
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var newItem = new CategoryViewModel();
                    ViewBag.CategoryId = new SelectList(db.Categories.Where(x => x.Status == "Active"), "CategoryId", "Description");
                    return View(newItem);
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", item.CategoryId);
                    TempData["CategoryData"] = new ProductViewModel()
                    {
                        ProductId = 0,
                        CategoryId = item.CategoryId,
                        Description = ""
                    };
                    return RedirectToAction("ProductSelection");
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    ProductViewModel item = TempData["CategoryData"] as ProductViewModel;
                    if (item == null)
                    {
                        return HttpNotFound();
                    }
                    records = _repository.RetrieveProductByCategory(item);
                    ViewBag.ProductId = new SelectList(records, "ProductId", "Description");

                    return View(item);
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
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
                    Session["Product"] = item;

                    return RedirectToAction("ItemSelection");
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    ItemViewModel item = TempData["ProductData"] as ItemViewModel;
                    if (item == null)
                    {
                        return HttpNotFound();
                    }
                    records = _repository.RetrieveItemByProduct(item);
                    ViewBag.ItemId = new SelectList(records, "ItemId", "Description");
                    return View(item);
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
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
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var item = new BrandViewModel();
                    records = _repository.RetrieveBrand();
                    ViewBag.BrandId = new SelectList(records, "BrandId", "Name");
                    return View(item);
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    records = _repository.RetrieveBrand();
                    var itemData = TempData["ModelData"] as ModelViewModel;
                    ViewBag.BrandId = new SelectList(records, "BrandId", "Name", item.BrandId);

                    TempData["ModelData"] = new ModelViewModel()
                    {
                        ModelId = 0,
                        ItemId = itemData.ItemId,
                        BrandId = item.BrandId,
                        ModelNumber = "",
                    };

                    return RedirectToAction("ModelSelection");
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    ModelViewModel item = TempData["ModelData"] as ModelViewModel;
                    if (item == null)
                    {
                        return HttpNotFound();
                    }
                    records = _repository.RetrieveModelByItemByBrand(item);
                    ViewBag.ModelId = new SelectList(records, "ModelId", "ModelNumber");
                    return View(item);
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    if (item == null)
                    {
                        return HttpNotFound();
                    }

                    records = _repository.RetrieveModelByItemByBrand(item);
                    ViewBag.ModelId = new SelectList(records, "ModelId", "ModelNumber", item.ModelId);

                    TempData["ModelSpecData"] = new ModelSpecViewModel()
                    {
                        ModelId = item.ModelId,
                        SpecificationId = -1,
                        Value = ""
                    };
                    Session["Model"] = item;

                    //return RedirectToAction("ModelSpecSelection");
                    return RedirectToAction("ModelSpecSelection");
                }
                return RedirectToAction("LoginSupplier", "Account");
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
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    ProductViewModel product = Session["Product"] as ProductViewModel;
                    ModelViewModel model = Session["Model"] as ModelViewModel;
                    var getSpec = from spec in db.Specifications
                                  join prodSpec in db.ProductSpecs on spec.SpecificationId equals prodSpec.SpecificationId
                                  join prod in db.Products on prodSpec.ProductId equals prod.ProductId
                                  where prod.ProductId == product.ProductId
                                  select spec;

                    var listSpecProduct = new List<SpecProductViewModel>();

                    foreach (var obj in getSpec)
                    {
                        var specProd = new SpecProductViewModel()
                        {
                            ModelId = model.ModelId,
                            SpecificationId = obj.SpecificationId,
                            Description = obj.Description,
                            TagId = obj.SpecialCatId,
                            Value = ""
                        };
                        listSpecProduct.Add(specProd);
                    }
                    return View(listSpecProduct);

                    //Runs a query to determine if the user is actually an "Supplier" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    //ModelSpecViewModel item = TempData["ModelSpecData"] as ModelSpecViewModel;
                    //if (item == null)
                    //{
                    //    return HttpNotFound();
                    //}
                    //records = _repository.RetrieveSpecification(item);
                    //return View(records);
                }
                return RedirectToAction("LoginSupplier", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ModelSpecSelection(FormCollection collection)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("LoginSupplier", "Account");
                }

                var getSupplierId = (from supplier in db.Suppliers
                                     where supplier.Id == userId
                                     select supplier.SupplierId).FirstOrDefault();

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");
                ModelViewModel model = Session["Model"] as ModelViewModel;
                if (User.IsInRole("Supplier"))
                {
                    int item = 0;
                    int supplierId = getSupplierId;
                    if (ModelState.IsValid)
                    {
                        var specIdArray = collection.GetValues("item.SpecificationId");
                        var valueArray = collection.GetValues("item.Value");

                        for (item = 0; item < valueArray.Count(); item++)
                        {
                            ModelSpecViewModel modelSpec = new ModelSpecViewModel();

                            modelSpec.Value = valueArray[item];
                            modelSpec.SpecificationId = Convert.ToInt32(specIdArray[item]);
                            modelSpec.ModelId = model.ModelId;
                            modelSpec.SupplierId = supplierId;
                            modelSpec.UserId = userId;
                            db.ModelSpecs.Add(modelSpec);
                            db.SaveChanges();
                        }
                        return Content("Success");
                    }

                    return View();
                }
                return RedirectToAction("LoginSupplier", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}