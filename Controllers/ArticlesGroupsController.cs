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
    public class ArticlesGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArticlesGroups
        public ActionResult Index()
        {
            return View();
        }

        [Route("ArticlesGroups/SubCategory/{Id?}")]
        public ActionResult SubCategory(int? Id)
        {
            List<ArticlesGroup> myparentgroups = new List<ArticlesGroup>();

            if (Id == null)
            {
                myparentgroups = db.ArticlesGroups.Where(item => item.ParentId == 0).ToList();
            }
            else
            {
                myparentgroups = db.ArticlesGroups.Where(item => item.ParentId == Id).ToList();
            }

            // If no Child Categories/Groups Display set of Products of the Group 
            if (myparentgroups.Count == 0)
            {
                return this.RedirectToAction("Index", "SearchProducts", new { CategoryId = Id });
            }            

            if (myparentgroups == null)
            {
                return HttpNotFound();
            }
            
            return View(myparentgroups);
        }

        // GET: ArticlesGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArticlesGroup articlesGroup = db.ArticlesGroups.Find(id);

            if (articlesGroup == null)
            {
                return HttpNotFound();
            }

            return View(articlesGroup);
        }

        // GET: ArticlesGroups/Create
        public ActionResult Create(int? id) // Create Sub Category for selected Category if parameter is passed
        {
            var articlesGroup = new ArticlesGroup();
            articlesGroup.ParentId = 0;
            
            if (id != null)
            {
                ArticlesGroup parentGroup = db.ArticlesGroups.Find(id);
                
                ViewBag.Parent = parentGroup.Name;

                articlesGroup.ParentId = (int)id;
            
            }
            
            return View(articlesGroup);                           
        }

        // POST: ArticlesGroups/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ArticlesGroup articlesGroup)
        {
            if (ModelState.IsValid)
            {

                HttpPostedFileBase Inputfile = Request.Files[0];

                if (Inputfile != null && Inputfile.ContentLength > 0)
                {
                    string filename = Path.GetFileName(Inputfile.FileName);
                    string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["MovieImage"].ToString()), filename);

                    articlesGroup.FileName = filename;
                    Inputfile.SaveAs(path);

                    articlesGroup.Image = new byte[Inputfile.ContentLength];
                    Inputfile.InputStream.Read(articlesGroup.Image, 0, Inputfile.ContentLength);
                }

                db.ArticlesGroups.Add(articlesGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }            

            return View(articlesGroup);
        }


        // GET: ArticlesGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArticlesGroup articlesGroup = db.ArticlesGroups.Find(id);

            if (articlesGroup == null)
            {
                return HttpNotFound();
            }

            return View(articlesGroup);
        }

        // POST: ArticlesGroups/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticlesGroup articlesGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articlesGroup).State = EntityState.Modified;

                var Inputfile = Request.Files[0];

                if (Inputfile != null && Inputfile.ContentLength > 0)
                {

                    string filename = Path.GetFileName(Inputfile.FileName);
                    string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["MovieImage"].ToString()), filename);

                    articlesGroup.FileName = filename;
                    Inputfile.SaveAs(path);

                    //Save Image in Binary Format. NB: need to figure out later how to display it in Datatable using this format
                    articlesGroup.Image = new byte[Inputfile.ContentLength];
                    Inputfile.InputStream.Read(articlesGroup.Image, 0, Inputfile.ContentLength);
                }
                else
                {
                    db.Entry(articlesGroup).Property("FileName").IsModified = false;
                    db.Entry(articlesGroup).Property("Image").IsModified = false;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articlesGroup);
        }

        // GET: ArticlesGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticlesGroup articlesGroup = db.ArticlesGroups.Find(id);
            if (articlesGroup == null)
            {
                return HttpNotFound();
            }
            return View(articlesGroup);
        }

        // POST: ArticlesGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticlesGroup articlesGroup = db.ArticlesGroups.Find(id);
            db.ArticlesGroups.Remove(articlesGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowImage(int id)
        {
            //var model = db.ArticlesGroups.Find(id);
            var model = GetModel(id);
            return File(model.Image, "image/jpg");
        }

        public ArticlesGroup GetModel(int id)
        {
            var model = db.ArticlesGroups.Find(id);
            return model;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
