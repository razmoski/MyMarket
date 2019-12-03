using SellMyStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellMyStuff.Controllers
{
    public class NavigationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Navbar
        public ActionResult TopNav()
        {
            var nav = new Navbar();
                        
            return PartialView("_topNav", nav.NavbarTop());
        }
    }
}