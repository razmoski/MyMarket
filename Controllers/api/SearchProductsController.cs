using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using SellMyStuff.Dtos;
using SellMyStuff.Models;

namespace SellMyStuff.Controllers.api
{
    public class SearchProductsController : ApiController
    {
        private Models.ApplicationDbContext db = new ApplicationDbContext();

        public SearchProductsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: SearchProducts
        public IEnumerable<ArticleDto> GetArticles(int? Id, string Colour, double? Price)
        {
                                    
            IEnumerable<ArticleDto> articles = new List<ArticleDto>();

            if (Id == null)
            {
                articles = db.Articles
                             .Include(a => a.ArticlesGroup)
                             .Include(i => i.Images)
                             .Select(Mapper.Map<Article, ArticleDto>);
            }
            else
            {
                articles = db.Articles
                             .Where(item => item.ArticlesGroupId == Id) 
                             .Where(item => item.Colour == Colour || Colour == null)
                             .Where(item => item.Price  <= Price || Price == null)
                             .Include(a => a.ArticlesGroup)
                             .Include(i => i.Images)
                             .Select(Mapper.Map<Article, ArticleDto>);
            }
            
            articles = articles.ToList();
            
            return articles;            

        }

        
    }
}