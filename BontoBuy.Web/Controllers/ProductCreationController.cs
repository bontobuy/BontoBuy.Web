using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.HelperMethods;
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
                    return RedirectToAction("Login", "Account");
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
                return RedirectToAction("Login", "Account");
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
                    return RedirectToAction("Login", "Account");
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
                return RedirectToAction("Login", "Account");
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
                    return RedirectToAction("Login", "Account");
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
                return RedirectToAction("Login", "Account");
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
                    return RedirectToAction("Login", "Account");
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
                return RedirectToAction("Login", "Account");
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
                    return RedirectToAction("Login", "Account");
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
                return RedirectToAction("Login", "Account");
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
                    return RedirectToAction("Login", "Account");
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
                    return RedirectToAction("ModelCreation");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ModelCreation()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    ModelViewModel model = TempData["ModelData"] as ModelViewModel;
                    SpecProductViewModel specProd = new SpecProductViewModel()
                    {
                        ItemId = model.ItemId,
                        Price = 0,
                        Status = "Pending",
                        SupplierId = (from s in db.Suppliers
                                      where s.Id == userId
                                      select s.SupplierId).FirstOrDefault(),
                        BrandName = "",
                        UserId = userId,
                        ModelNumber = "",
                        NumberDaysToAdvert = 1,
                        DeliveryInDays = 1
                    };
                    Session["SpecProd"] = specProd;
                    return View(specProd);

                    //return RedirectToAction("ModelSpecCreation");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ModelCreation(SpecProductViewModel item, HttpPostedFileBase modelPhoto)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    var photo = UploadPhoto(modelPhoto, item);
                    if (photo != null)
                    {
                        SpecProductViewModel specProd = Session["SpecProd"] as SpecProductViewModel;
                        int brandId = 0;

                        //This object will allow us to use the convert to ConvertToTitleCase method
                        var helper = new Helper();

                        //Convert to Title Case
                        item.BrandName = helper.ConvertToTitleCase(item.BrandName);

                        //Check if brand name exists
                        var brand = (from b in db.Brands
                                     where b.Name == item.BrandName
                                     select b).FirstOrDefault();

                        if (brand == null)
                        {
                            var newBrand = new BrandViewModel()
                            {
                                Name = item.BrandName
                            };
                            db.Brands.Add(newBrand);
                            db.SaveChanges();
                            brandId = newBrand.BrandId;
                        }
                        if (brand != null)
                        {
                            brandId = brand.BrandId;
                        }

                        var model = new ModelViewModel();

                        model.Price = item.Price;
                        model.Status = specProd.Status;
                        model.UserId = userId;
                        model.SupplierId = specProd.SupplierId;
                        model.ItemId = specProd.ItemId;
                        model.BrandId = brandId;
                        model.ModelNumber = item.ModelNumber;
                        model.DtCreated = DateTime.UtcNow;
                        model.NumberDaysToAdvert = item.NumberDaysToAdvert;
                        model.DeliveryInDays = item.DeliveryInDays;
                        db.Models.Add(model);
                        db.SaveChanges();

                        var modelCommission = new ModelCommissionViewModel()
                        {
                            ModelId = model.ModelId
                        };
                        string commissionName = "Tier1";
                        if (model.NumberDaysToAdvert >= 1 && model.NumberDaysToAdvert < 6)
                            commissionName = "Tier1";
                        if (model.NumberDaysToAdvert >= 6 && model.NumberDaysToAdvert < 20)
                            commissionName = "Tier2";
                        if (model.NumberDaysToAdvert >= 20)
                            commissionName = "Tier3";

                        int commissionId = (from c in db.Commissions
                                            where c.Name == commissionName
                                            select c.CommissionId).FirstOrDefault();

                        modelCommission.CommissionId = commissionId;

                        db.ModelCommissions.Add(modelCommission);
                        db.SaveChanges();

                        var photoModel = new PhotoModelViewModel()
                        {
                            PhotoId = photo.PhotoId,
                            ModelId = model.ModelId
                        };
                        db.PhotoModels.Add(photoModel);
                        db.SaveChanges();

                        Session["ModelSpecData"] = model;

                        return RedirectToAction("ModelSpecCreation");

                        //return RedirectToAction("ModelSpecCreation");on("ModelSpecCreation");
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ModelSpecCreation()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");

                if (User.IsInRole("Supplier"))
                {
                    ProductViewModel product = Session["Product"] as ProductViewModel;
                    ModelViewModel item = Session["ModelSpecData"] as ModelViewModel;

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
                            ModelNumber = "",
                            BrandName = "",
                            ItemId = item.ItemId,
                            SpecificationId = obj.SpecificationId,
                            SpecDescription = obj.Description,
                            SpecialCatId = obj.SpecialCatId,
                            Value = ""
                        };
                        listSpecProduct.Add(specProd);
                    }
                    return View(listSpecProduct);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ModelSpecCreation(FormCollection collection)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var getSupplierId = (from supplier in db.Suppliers
                                     where supplier.Id == userId
                                     select supplier.SupplierId).FirstOrDefault();

                //Check if the "Supplier" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Supplier");
                ModelViewModel model = Session["ModelSpecData"] as ModelViewModel;
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
                            if (String.IsNullOrWhiteSpace(valueArray[item]))
                            {
                                valueArray[item] = "n/a";
                            }
                            modelSpec.Value = valueArray[item];
                            modelSpec.SpecificationId = Convert.ToInt32(specIdArray[item]);
                            modelSpec.ModelId = model.ModelId;
                            modelSpec.SupplierId = supplierId;
                            modelSpec.UserId = userId;
                            db.ModelSpecs.Add(modelSpec);
                            db.SaveChanges();
                        }
                        return RedirectToAction("SupplierRetrieveModels", "Supplier");
                    }

                    return View();
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        private PhotoViewModel UploadPhoto(HttpPostedFileBase file, SpecProductViewModel item)
        {
            if (file != null && file.ContentLength < 1 * 1024 * 1024)
            {
                string imageName = Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Images/" + imageName);

                //Save the Image in the Images Folder
                file.SaveAs(physicalPath);

                //Save the record in the database
                var photo = new PhotoViewModel()
                {
                    Name = item.ModelNumber,
                    ImageUrl = imageName,
                    PhysicalPath = physicalPath
                };
                db.Photos.Add(photo);
                db.SaveChanges();

                return photo;
            }
            return null;
        }
    }
}