using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class AdminController : NotificationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                GetNewSupplierActivation();
                GetNewModelsActivation();
                return View();
            }
            if (User.IsInRole("Supplier"))
            {
                return RedirectToAction("Index", "Supplier");
            }
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RetrievePendingModels()
        {
            var pendingModels = from m in db.Models
                                where m.Status == "Pending"
                                select m;
            GetNewSupplierActivation();
            GetNewModelsActivation();
            return View(pendingModels);
        }

        public ActionResult EditModelStatus(int id)
        {
            int modelId = id;
            GetNewSupplierActivation();
            GetNewModelsActivation();
            ViewBag.StatusId = new SelectList(db.Categories.Where(x => x.Status == "Active"), "CategoryId", "Description");
            return null;
        }

        public ActionResult OrderReturnReport()
        {
            //Retrieve last 5 months data for Orders
            var minDate = DateTime.Now.AddMonths(-5);
            var maxDate = minDate.AddMonths(1);
            var dataLast5Month = (from o in db.Orders
                                  where o.DtCreated > minDate && o.DtCreated < maxDate
                                  orderby o.OrderId
                                  select o).ToList().Count();

            var minDateLast4Month = DateTime.Now.AddMonths(-4);
            var maxDateLast4Month = minDateLast4Month.AddMonths(1);
            var dataLast4Month = (from o in db.Orders
                                  where o.DtCreated > minDateLast4Month && o.DtCreated < maxDateLast4Month
                                  orderby o.OrderId
                                  select o).ToList().Count();

            var minDateLast3Month = DateTime.Now.AddMonths(-3);
            var maxDateLast3Month = minDateLast3Month.AddMonths(1);
            var dataLast3Month = (from o in db.Orders
                                  where o.DtCreated > minDateLast3Month && o.DtCreated < maxDateLast3Month
                                  orderby o.OrderId
                                  select o).ToList().Count();

            var minDateLast2Month = DateTime.Now.AddMonths(-2);
            var maxDateLast2Month = minDateLast2Month.AddMonths(1);
            var dataLast2Month = (from o in db.Orders
                                  where o.DtCreated > minDateLast2Month && o.DtCreated < maxDateLast2Month
                                  orderby o.OrderId
                                  select o).ToList().Count();

            var minDateLastMonth = DateTime.Now.AddMonths(-1);
            var maxDateLastMonth = minDateLastMonth.AddMonths(1);
            var dataLastMonth = (from o in db.Orders
                                 where o.DtCreated > minDateLastMonth && o.DtCreated < maxDateLastMonth
                                 orderby o.OrderId
                                 select o).ToList().Count();

            //Retrieve last 5 months data for Returns
            var minReturnDate = DateTime.Now.AddMonths(-5);
            var maxReturnDate = minReturnDate.AddMonths(1);
            var dataLast5MonthReturn = (from o in db.Returns
                                        where o.DtCreated > minReturnDate && o.DtCreated < maxReturnDate
                                        orderby o.ReturnId
                                        select o).ToList().Count();

            var minDateLast4MonthReturn = DateTime.Now.AddMonths(-4);
            var maxDateLast4MonthReturn = minDateLast4MonthReturn.AddMonths(1);
            var dataLast4MonthReturn = (from o in db.Returns
                                        where o.DtCreated > minDateLast4MonthReturn && o.DtCreated < maxDateLast4MonthReturn
                                        orderby o.ReturnId
                                        select o).ToList().Count();

            var minDateLast3MonthReturn = DateTime.Now.AddMonths(-3);
            var maxDateLast3MonthReturn = minDateLast3MonthReturn.AddMonths(1);
            var dataLast3MonthReturn = (from o in db.Returns
                                        where o.DtCreated > minDateLast3MonthReturn && o.DtCreated < maxDateLast3MonthReturn
                                        orderby o.ReturnId
                                        select o).ToList().Count();

            var minDateLast2MonthReturn = DateTime.Now.AddMonths(-2);
            var maxDateLast2MonthReturn = minDateLast2MonthReturn.AddMonths(1);
            var dataLast2MonthReturn = (from o in db.Returns
                                        where o.DtCreated > minDateLast2MonthReturn && o.DtCreated < maxDateLast2MonthReturn
                                        orderby o.ReturnId
                                        select o).ToList().Count();

            var minDateLastMonthReturn = DateTime.Now.AddMonths(-1);
            var maxDateLastMonthReturn = minDateLastMonthReturn.AddMonths(1);
            var dataLastMonthReturn = (from o in db.Returns
                                       where o.DtCreated > minDateLastMonthReturn && o.DtCreated < maxDateLastMonthReturn
                                       orderby o.ReturnId
                                       select o).ToList().Count();

            var myChart = new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddTitle("Order vs Return")
                .AddSeries(
                chartType: "Column",
                    name: "Orders",
                    xValue: new[] { minDate.ToString("MMMM"), minDateLast4Month.ToString("MMMM"), minDateLast3Month.ToString("MMMM"), minDateLast2Month.ToString("MMMM"), minDateLastMonth.ToString("MMMM") },
                    yValues: new[] { dataLast5Month, dataLast4Month, dataLast3Month, dataLast2Month, dataLastMonth })
                    .AddLegend()
                .AddSeries(
                chartType: "Column",
                    name: "Returns",
                    xValue: new[] { minReturnDate.ToString("MMMM"), minDateLast4MonthReturn.ToString("MMMM"), minDateLast3MonthReturn.ToString("MMMM"), minDateLast2MonthReturn.ToString("MMMM"), minDateLastMonthReturn.ToString("MMMM") },
                    yValues: new[] { dataLast5MonthReturn, dataLast4MonthReturn, dataLast3MonthReturn, dataLast2MonthReturn, dataLastMonthReturn })
                    .AddLegend()
                .GetBytes("png");

            return File(myChart, "image/bytes");
        }

        public ActionResult NewSuppliers()
        {
            var records = db.Users.Where(u =>
         u.Roles.Join(db.Roles, usrRole => usrRole.RoleId,
         role => role.Id, (usrRole, role) => role).Any(r => r.Name.Equals("Supplier")) && u.Status == "Pending").ToList();

            var userAccountList = new List<UserRoleViewModel>();
            foreach (var item in records)
            {
                var userAccount = new UserRoleViewModel()
                {
                    UserId = item.Id,
                    Email = item.Email,
                    Status = item.Status
                };
                userAccountList.Add(userAccount);
            }

            return View("../Role/RetrieveSuppliers", userAccountList);
        }
    }
}