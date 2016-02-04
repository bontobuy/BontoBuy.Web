﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("LoginAdmin", "Account");
        }
    }
}