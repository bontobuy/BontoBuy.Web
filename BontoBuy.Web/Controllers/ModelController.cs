using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BontoBuy.Web.HelperMethods;
using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;

namespace BontoBuy.Web.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelRepo _repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        private Helper helper = new Helper();

        public enum ManageMessageId
        {
            AddSuccess,
            UpdateSuccess,
            ArchiveSuccess,
            RestoreSuccess,
            Error
        }

        public ModelController(IModelRepo repo)
        {
            _repository = repo;
        }

        public ActionResult RetrieveActive(string searchString, ManageMessageId? message)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Admin"))
                {
                    var records = db.Models.Where(x => x.Status == "Active").ToList();
                    var modelList = new List<ModelAdminRetrieveViewModel>();

                    foreach (var item in records)
                    {
                        var commission = (((from c in db.Commissions
                                            join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                            where mc.ModelId == item.ModelId
                                            select c.Percentage).FirstOrDefault()));

                        var modelItem = new ModelAdminRetrieveViewModel()
                        {
                            ModelId = item.ModelId,
                            ModelNumber = item.ModelNumber,
                            DtCreated = item.DtCreated,
                            Status = item.Status,
                            ImageUrl = (from ph in db.Photos
                                        join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                        join m in db.Models on pm.ModelId equals m.ModelId
                                        where pm.ModelId == item.ModelId
                                        select ph.ImageUrl).FirstOrDefault(),
                            Price = item.Price,
                            SupplierId = item.SupplierId,
                            SupplierCommission = Convert.ToInt32((commission * item.Price) / 100),
                            CommissionPercentage = commission + " %",
                            SupplierName = (from s in db.Suppliers
                                            where s.SupplierId == item.SupplierId
                                            select s.Name).FirstOrDefault()
                        };
                        if (!String.IsNullOrWhiteSpace(searchString))
                        {
                            var properString = helper.ConvertToTitleCase(searchString);
                            if (modelItem.SupplierName == properString)
                                modelList.Add(modelItem);
                        }
                        if (String.IsNullOrWhiteSpace(searchString))
                            modelList.Add(modelItem);
                    }

                    //if (records == null)
                    //{
                    //    return HttpNotFound();
                    //}

                    ViewBag.StatusMessage =
                message == ManageMessageId.AddSuccess ? "You have successfully added a new Model."
                : message == ManageMessageId.ArchiveSuccess ? "You have just archive a Model."
                : message == ManageMessageId.UpdateSuccess ? "You have successfully activated a Model."
                : message == ManageMessageId.RestoreSuccess ? "You have successfully restore a Model."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                    return View(modelList.OrderByDescending(x => x.Status));
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrievePending(string searchString, ManageMessageId? message)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Admin"))
                {
                    //var records = _repository.Retrieve();
                    var records = db.Models.Where(x => x.Status == "Pending").ToList();
                    var modelList = new List<ModelAdminRetrieveViewModel>();

                    foreach (var item in records)
                    {
                        var commission = (((from c in db.Commissions
                                            join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                            where mc.ModelId == item.ModelId
                                            select c.Percentage).FirstOrDefault()));

                        var modelItem = new ModelAdminRetrieveViewModel()
                        {
                            ModelId = item.ModelId,
                            ModelNumber = item.ModelNumber,
                            DtCreated = item.DtCreated,
                            Status = item.Status,
                            ImageUrl = (from ph in db.Photos
                                        join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                        join m in db.Models on pm.ModelId equals m.ModelId
                                        where pm.ModelId == item.ModelId
                                        select ph.ImageUrl).FirstOrDefault(),
                            Price = item.Price,
                            SupplierId = item.SupplierId,
                            SupplierCommission = Convert.ToInt32((commission * item.Price) / 100),
                            CommissionPercentage = commission + " %",
                            SupplierName = (from s in db.Suppliers
                                            where s.SupplierId == item.SupplierId
                                            select s.Name).FirstOrDefault()
                        };
                        if (!String.IsNullOrWhiteSpace(searchString))
                        {
                            var properString = helper.ConvertToTitleCase(searchString);
                            if (modelItem.SupplierName == properString)
                                modelList.Add(modelItem);
                        }
                        if (String.IsNullOrWhiteSpace(searchString))
                            modelList.Add(modelItem);
                    }

                    //if (records == null)
                    //{
                    //    return HttpNotFound();
                    //}

                    ViewBag.StatusMessage =
                message == ManageMessageId.AddSuccess ? "You have successfully added a new Model."
                : message == ManageMessageId.ArchiveSuccess ? "You have just archive a Model."
                : message == ManageMessageId.UpdateSuccess ? "You have successfully activated a Model."
                : message == ManageMessageId.RestoreSuccess ? "You have successfully restore a Model."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                    return View(modelList.OrderByDescending(x => x.Status));
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Model
        public ActionResult Retrieve(string searchString, ManageMessageId? message)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (User.IsInRole("Admin"))
                {
                    //var records = _repository.Retrieve();
                    var records = db.Models.ToList();
                    var modelList = new List<ModelAdminRetrieveViewModel>();

                    foreach (var item in records)
                    {
                        var commission = (((from c in db.Commissions
                                            join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                            where mc.ModelId == item.ModelId
                                            select c.Percentage).FirstOrDefault()));

                        var modelItem = new ModelAdminRetrieveViewModel()
                        {
                            ModelId = item.ModelId,
                            ModelNumber = item.ModelNumber,
                            DtCreated = item.DtCreated,
                            Status = item.Status,
                            ImageUrl = (from ph in db.Photos
                                        join pm in db.PhotoModels on ph.PhotoId equals pm.PhotoId
                                        join m in db.Models on pm.ModelId equals m.ModelId
                                        where pm.ModelId == item.ModelId
                                        select ph.ImageUrl).FirstOrDefault(),
                            Price = item.Price,
                            SupplierId = item.SupplierId,
                            SupplierCommission = Convert.ToInt32((commission * item.Price) / 100),
                            CommissionPercentage = commission + " %",
                            SupplierName = (from s in db.Suppliers
                                            where s.SupplierId == item.SupplierId
                                            select s.Name).FirstOrDefault()
                        };
                        if (!String.IsNullOrWhiteSpace(searchString))
                        {
                            var properString = helper.ConvertToTitleCase(searchString);
                            if (modelItem.SupplierName == properString)
                                modelList.Add(modelItem);
                        }
                        if (String.IsNullOrWhiteSpace(searchString))
                            modelList.Add(modelItem);
                    }

                    //if (records == null)
                    //{
                    //    return HttpNotFound();
                    //}

                    ViewBag.StatusMessage =
                message == ManageMessageId.AddSuccess ? "You have successfully added a new Model."
                : message == ManageMessageId.ArchiveSuccess ? "You have just archive a Model."
                : message == ManageMessageId.UpdateSuccess ? "You have successfully activated a Model."
                : message == ManageMessageId.RestoreSuccess ? "You have successfully restore a Model."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

                    return View(modelList.OrderByDescending(x => x.Status));
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Model/Details/5
        public ActionResult Get(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                //var role = db.roles.singleordefault(m => m.name == "admin");

                if (User.IsInRole("Admin"))
                {
                    if (id < 1)
                    {
                        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product Id cannot be null or empty!");
                        RedirectToAction("Retrieve");
                    }

                    var profile = _repository.Get(id);
                    if (profile == null)
                    {
                        return HttpNotFound();
                    }

                    return View(profile);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Model/Create
        public ActionResult Create()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                //var role = db.roles.singleordefault(m => m.name == "admin");

                if (User.IsInRole("Admin"))
                {
                    var newItem = new ModelViewModel();
                    ViewBag.BrandId = new SelectList(db.Brands.Where(x => x.Status == "Active"), "BrandId", "Name");
                    ViewBag.ItemId = new SelectList(db.Items.Where(x => x.AdminStatus == "Active"), "ItemId", "Description");
                    return View(newItem);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Model/Create
        [HttpPost]
        public ActionResult Create(ModelViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                //var role = db.roles.singleordefault(m => m.name == "admin");

                if (User.IsInRole("Admin"))
                {
                    if (item == null)
                    {
                        return RedirectToAction("Retrieve", new { message = ManageMessageId.Error });
                    }

                    // var newItem = _repository.Create(item);
                    if (ModelState.IsValid)
                    {
                        ViewBag.BrandId = new SelectList(db.Brands.Where(x => x.Status == "Active"), "BrandId", "Name", item.BrandId);
                        ViewBag.ItemId = new SelectList(db.Items.Where(x => x.AdminStatus == "Active"), "ItemId", "Description", item.ItemId);

                        item.Status = "Active";
                        db.Models.Add(item);
                        db.SaveChanges();
                        return RedirectToAction("Retrieve", new { message = ManageMessageId.AddSuccess });
                    }

                    return RedirectToAction("Retrieve", new { message = ManageMessageId.Error });
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Model/Edit/5
        public ActionResult Update(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                //var role = db.roles.singleordefault(m => m.name == "admin");

                if (User.IsInRole("Admin"))
                {
                    //  ViewBag.ItemId = new SelectList(db.Items.Where(x => x.Status == "Active"), "ItemId", "Description");

                    if (id < 1)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Identifier");
                    }

                    var itemToUpdate = db.Models.Where(x => x.ModelId == id).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        return HttpNotFound();
                    }
                    var model = new ModelAdminViewModel()
                    {
                        ModelNumber = itemToUpdate.ModelNumber,
                        ModelId = itemToUpdate.ModelId,
                        DtCreated = itemToUpdate.DtCreated,
                        Price = itemToUpdate.Price,
                        Status = itemToUpdate.Status,
                        BrandName = (from b in db.Brands
                                     where b.BrandId == itemToUpdate.BrandId
                                     select b.Name).FirstOrDefault(),
                        ItemName = (from i in db.Items
                                    where i.ItemId == itemToUpdate.ItemId
                                    select i.Description).FirstOrDefault(),
                        SupplierName = (from s in db.Suppliers
                                        where s.SupplierId == itemToUpdate.SupplierId
                                        select s.Name).FirstOrDefault(),
                        SupplierEmail = (from s in db.Suppliers
                                         where s.SupplierId == itemToUpdate.SupplierId
                                         select s.Email).FirstOrDefault(),
                        SupplierId = itemToUpdate.SupplierId
                    };

                    Session["Model"] = model;
                    return View(model);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Model/Edit/5
        [HttpPost]
        public async Task<ActionResult> Update(int id, ModelAdminViewModel model)
        {
            try
            {
                var item = Session["Model"] as ModelAdminViewModel;
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                //var role = db.roles.singleordefault(m => m.name == "admin");

                if (User.IsInRole("Admin"))
                {
                    // ViewBag.ItemId = new SelectList(db.Items.Where(x => x.Status == "Active"), "ItemId", "Description", item.ItemId);
                    if (item == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product cannot be null");
                    }

                    ////var updatedItem = _repository.Update(id, item);
                    //if (updatedItem == null)
                    //{
                    //    return HttpNotFound();
                    //}

                    var itemToUpdate = db.Models.Where(x => x.ModelId == item.ModelId).FirstOrDefault();
                    itemToUpdate.Status = "Active";

                    db.SaveChanges();

                    if (itemToUpdate.Status == "Active")
                    {
                        var supplierEmail = (from s in db.Suppliers
                                             where s.SupplierId == item.SupplierId
                                             select s.Email).FirstOrDefault();

                        var body = "<p>Dear Valued Supplier,</p><p>Your product with reference {0} has been validated</p><p>Best Regards</p><p>The BontoBuy Team</p>";
                        var message = new MailMessage();
                        message.To.Add(new MailAddress(supplierEmail));
                        message.From = new MailAddress("bontobuy@gmail.com");
                        message.Subject = "Register on BontoBuy";
                        message.Body = string.Format(body, item.ModelNumber);
                        message.IsBodyHtml = true;

                        var smtp = new SmtpClient();

                        var credential = new NetworkCredential()
                        {
                            UserName = "bontobuy@gmail.com",
                            Password = "b0nt0@dmin"
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);
                    }

                    return RedirectToAction("Retrieve", new { message = ManageMessageId.UpdateSuccess });
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Model/Delete/5
        public ActionResult Archive(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    if (id < 1)
                    {
                        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product Id cannot be null or empty!");
                        RedirectToAction("Retrieve");
                    }

                    var profile = _repository.Get(id);
                    if (profile == null)
                    {
                        return HttpNotFound();
                    }

                    return View(profile);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Model/Delete/5
        [HttpPost]
        public ActionResult Archive(ModelViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var specId = item.ModelId;
                    if (specId < 1)
                    {
                        RedirectToAction("Retrieve", new { message = ManageMessageId.Error });
                    }
                    if (item == null)
                    {
                        RedirectToAction("Retrieve", new { message = ManageMessageId.Error });
                    }
                    _repository.Archive(specId);

                    //   return RedirectToAction("Retrieve");
                    return RedirectToAction("Retrieve", new { message = ManageMessageId.ArchiveSuccess });
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RetrieveArchives(ManageMessageId? message)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var records = _repository.RetrieveArchives();
                    if (records == null)
                    {
                        return HttpNotFound();
                    }

                    ViewBag.StatusMessage =
         message == ManageMessageId.RestoreSuccess ? "You have successfully restore a Model."
         : message == ManageMessageId.Error ? "An error has occurred."
         : "";

                    return View(records);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult RevertArchive(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    if (id < 1)
                    {
                        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product Id cannot be null or empty!");
                        RedirectToAction("RetrieveArchives");
                    }

                    var profile = _repository.Get(id);
                    if (profile == null)
                    {
                        return HttpNotFound();
                    }

                    return View(profile);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult RevertArchive(ModelViewModel item)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //Check if the "Admin" role exists if not it returns a null value
                var role = db.Roles.SingleOrDefault(m => m.Name == "Admin");

                if (User.IsInRole("Admin"))
                {
                    //Runs a query to determine if the user is actually an "Admin" if not it returns a null value
                    //var userInRole = db.Users.Where(m => m.Roles.Any(r => r.UserId == userId)).FirstOrDefault();
                    //if (userInRole != null)
                    //{
                    var specId = item.ModelId;
                    if (specId < 1)
                    {
                        RedirectToAction("RetrieveArchives");
                    }
                    if (item == null)
                    {
                        RedirectToAction("RetrieveArchives", new { message = ManageMessageId.Error });
                    }

                    _repository.RevertArchive(specId);

                    //   return RedirectToAction("Retrieve");
                    return RedirectToAction("RetrieveArchives", new { message = ManageMessageId.RestoreSuccess });
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