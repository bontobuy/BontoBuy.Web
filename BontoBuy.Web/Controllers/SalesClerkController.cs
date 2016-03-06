using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class SalesClerkController : Controller
    {
        // GET: SalesClerk
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RetrieveOrders()
        {
            return View();
        }
    }
}