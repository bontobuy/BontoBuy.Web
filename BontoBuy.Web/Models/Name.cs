using BontoBuy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BontoBuy.Web.Models
{
    public class Name
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string name { get; set; }
        public string GetName(string id)
        {
            string name = db.Users.Where(x => x.Id == id).Select(x => x.Name).Single();
            return name;
        }
    }
}