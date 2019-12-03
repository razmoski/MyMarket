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
using AutoMapper;
using SellMyStuff.Dtos;
using SellMyStuff.Models;

namespace SellMyStuff.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View();
        }

        //[Route("Articles/Filter/{CategoryId}")]
        public ActionResult Filter(int CategoryId)
        {
            var articles = db.Articles
                             .Where(item => item.ArticlesGroupId == CategoryId)
                             .Include(a => a.ArticlesGroup)                             
                             .Include(i => i.Images)
                             .ToList();
            return View(articles);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Article article = db.Articles.Include(i => i.Images).SingleOrDefault(x => x.Id == id);

            if (article == null)
            {
                return HttpNotFound();
            }
            
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.ArticlesGroupId = new SelectList(db.ArticlesGroups, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.Images = SaveMyImages(article.Id);
                
                db.Articles.Add(article);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
                        
            ViewBag.ArticlesGroupId = new SelectList(db.ArticlesGroups, "Id", "Name", article.ArticlesGroupId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Include(i => i.Images).SingleOrDefault(x => x.Id == id);

            if (article == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticlesGroupId = new SelectList(db.ArticlesGroups, "Id", "Name", article.ArticlesGroupId);
            
            return View(article);
        }

        // POST: Articles/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                
                if (Request.Files.Count != 0)
                { SaveMyImages(article.Id); }

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticlesGroupId = new SelectList(db.ArticlesGroups, "Id", "Name", article.ArticlesGroupId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult DeletePhoto(int id)
        {
            if (id == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                
                Images image = db.Images.Find(id);
                if (image == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Delete file from the file system                
                if (System.IO.File.Exists(image.SrcPath))
                {
                    System.IO.File.Delete(image.SrcPath);
                }
                
                //Remove from database
                db.Images.Remove(image);
                db.SaveChanges();
                
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public List<Images> SaveMyImages(int articleId)
        {
            List<Images> images = new List<Images>();

            //Get file details from current request    
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var Inputfile = Request.Files[i];

                if (Inputfile != null && Inputfile.ContentLength > 0)
                {
                    string filename = Path.GetFileName(Inputfile.FileName);
                    string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["MovieImage"].ToString()), filename);

                    Images image = new Images()
                    {
                        SrcPath = path,
                        FileName = filename,  // Photos/filename.jpg
                        //Sequence = i + 1,   // Not Used or at least for now
                        ArticleId = articleId  //article.Id
                    };

                    images.Add(image);

                    Inputfile.SaveAs(path);
                    db.Entry(image).State = EntityState.Added;
                }
            }
            return images;
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
