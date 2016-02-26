using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace BontoBuy.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewPdf()
        {
            return new ViewAsPdf("Invoice");
        }

        public ActionResult Invoice()
        {
            return View();
        }
    }
}