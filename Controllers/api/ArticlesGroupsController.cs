using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using AutoMapper;
using SellMyStuff.Dtos;
using SellMyStuff.Models;

namespace SellMyStuff.Controllers.API
{
    public class ArticlesGroupsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ArticlesGroups
        public IEnumerable<ArticlesGroupDto> GetArticlesGroups()
        {
            db.ArticlesGroups.ToList();
            return db.ArticlesGroups.ToList().Select(Mapper.Map<ArticlesGroup, ArticlesGroupDto>);
        
        }


        // GET: api/ArticlesGroups/5
        [ResponseType(typeof(ArticlesGroup))]
        public IHttpActionResult GetArticlesGroup(int id)
        {
            ArticlesGroup articlesGroup = db.ArticlesGroups.Find(id);
            if (articlesGroup == null)
            {
                return NotFound();
            }

            return Ok(articlesGroup);
        }

        // PUT: api/ArticlesGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticlesGroup(int id, ArticlesGroup articlesGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articlesGroup.Id)
            {
                return BadRequest();
            }

            db.Entry(articlesGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlesGroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ArticlesGroups
        [ResponseType(typeof(ArticlesGroup))]
        public IHttpActionResult PostArticlesGroup(ArticlesGroup articlesGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ArticlesGroups.Add(articlesGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = articlesGroup.Id }, articlesGroup);
        }

        // DELETE: api/ArticlesGroups/5
        [ResponseType(typeof(ArticlesGroup))]
        public IHttpActionResult DeleteArticlesGroup(int id)
        {
            ArticlesGroup articlesGroup = db.ArticlesGroups.Find(id);
            if (articlesGroup == null)
            {
                return NotFound();
            }

            db.ArticlesGroups.Remove(articlesGroup);
            db.SaveChanges();

            return Ok(articlesGroup);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticlesGroupExists(int id)
        {
            return db.ArticlesGroups.Count(e => e.Id == id) > 0;
        }
    }
}