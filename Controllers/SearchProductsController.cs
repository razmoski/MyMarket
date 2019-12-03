using SellMyStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellMyStuff.Controllers
{
    public class SearchProductsController : Controller
    {
        private Models.ApplicationDbContext db = new ApplicationDbContext();

        // GET: SearchProducts
        public ActionResult Index(int CategoryId)
        {
            var Group = db.ArticlesGroups.FirstOrDefault(g => g.Id == CategoryId);
            
            ViewBag.CategoryId   = CategoryId;
            ViewBag.CategoryName = Group.Name;

            return View();
        }
    }
}