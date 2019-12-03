using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellMyStuff.Models;

namespace SellMyStuff.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private int articleId {get; set;}
    
    // GET: Images
    // @Url.Action("ShowImages", "Images", new {articleId=11});

    public ActionResult Index(string articleId)
        {
        
            var images = db.Images.ToList();

            // this need reviewing
            if (articleId != null)
            {
                var artId = int.Parse(articleId);

                if (artId != 0)
                {
                    images = db.Images.Where(i => i.ArticleId == artId).ToList();
                }
                ViewBag.ArticleId = artId;
                TempData["ArticleId"] = artId;
                TempData.Keep("ArticleId");

                this.articleId = artId;
            }
            
            return View(images);
        }
        
        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Sequence,SrcPath,FileName,ArticleId")] Images images)
        {
            if (ModelState.IsValid)
            {
                if (this.articleId != 0)
                {
                    //Images artImages = db.Images.Where(i => i.ArticleId == this.articleId);

                    images.ArticleId = this.articleId;
                    //images.Sequence = artImages.Count();
                }
                
                db.Images.Add(images);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(images);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sequence,SrcPath,FileName,ArticleId")] Images images)
        {
            if (ModelState.IsValid)
            {
                db.Entry(images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Images images = db.Images.Find(id);
            db.Images.Remove(images);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RedirectToArticle()
        {

            

            if (TempData["ArticleId"] != null )
            {
                var ArtId = TempData["ArticleId"];

                TempData.Remove("ArticleId");
                return RedirectToAction("Edit","Articles", new { id = ArtId });
            }

            return View();
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
