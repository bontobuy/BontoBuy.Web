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
    public class SupplierController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Supplier
        public ActionResult Index()
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
                    return View();
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult SupplierRetrieveModels()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    var getSupplier = (from s in db.Suppliers
                                       where s.Id == userId
                                       select s).FirstOrDefault();

                    int supplierId = getSupplier.SupplierId;

                    var retrieveModels = (from m in db.Models
                                          join ms in db.ModelSpecs on m.ModelId equals ms.ModelId
                                          join s in db.Suppliers on ms.SupplierId equals s.SupplierId
                                          where ms.SupplierId == supplierId
                                          select m).Distinct();

                    var modelList = new List<SupplierRetrieveModelsViewModel>();

                    foreach (var obj in retrieveModels)
                    {
                        var model = new SupplierRetrieveModelsViewModel()
                        {
                            SupplierId = supplierId,
                            ModelId = obj.ModelId,
                            ModelNumber = obj.ModelNumber,
                            BrandName = (from b in db.Brands
                                         join m in db.Models on b.BrandId equals m.BrandId
                                         where b.BrandId == obj.BrandId
                                         select b.Name).FirstOrDefault(),
                            ImageUrl = (from ph in db.Photos
                                        join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                        join m in db.Models on pm.ModelId equals m.ModelId
                                        where pm.ModelId == obj.ModelId
                                        select ph.ImageUrl).FirstOrDefault()
                        };

                        modelList.Add(model);
                    }

                    return View(modelList);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult SupplierGetModelSpec(int id)
        {
            try
            {
                int modelId = id;

                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Supplier"))
                {
                    var getSupplier = (from s in db.Suppliers
                                       where s.Id == userId
                                       select s).FirstOrDefault();

                    var modelSpecList = (from ms in db.ModelSpecs
                                         where ms.SupplierId == getSupplier.SupplierId
                                         && ms.ModelId == modelId
                                         select ms).ToList();

                    var modelSpecVM = new List<SupplierGetModelSpecViewModel>();

                    foreach (var obj in modelSpecList)
                    {
                        var model = new SupplierGetModelSpecViewModel()
                        {
                            SupplierId = getSupplier.SupplierId,
                            ModelId = modelId,
                            Value = obj.Value,
                            Description = (from s in db.Specifications
                                           join ms in db.ModelSpecs on s.SpecificationId equals ms.SpecificationId
                                           where s.SpecificationId == obj.SpecificationId
                                           select s.Description).FirstOrDefault()
                        };
                        ViewBag.ModelSpec = db.Models.Find(modelId).ModelNumber.ToString();
                        modelSpecVM.Add(model);
                    }

                    return View(modelSpecVM);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}