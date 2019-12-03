using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellMyStuff.Models;

namespace SellMyStuff.Controllers
{
    //[PassParametersDuringRedirect]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        //[Route("Home/Index/{Id?}")]
        public ActionResult Index(int? myId)
        {
            List<ArticlesGroup> myparentgroups = new List<ArticlesGroup>();

            if (myId == null){
                myparentgroups = db.ArticlesGroups.Where(item => item.ParentId == 0).ToList();
            }
            else
            {
                myparentgroups = db.ArticlesGroups.Where(item => item.ParentId == myId).ToList();
            }
                                       
            if (myparentgroups == null)
            {                
                return HttpNotFound();
            }
            
            return View(myparentgroups);
        }

        // [HttpPost]
        [Route("Home/Dive/{Id}")]
        public ActionResult Dive(int Id)
        {
            
            return this.RedirectToAction("SubCategory", "ArticlesGroups", new { id = Id });
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}