using AutoMapper;
using SellMyStuff.Dtos;
using SellMyStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellMyStuff.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // API -> Outbound (Send data to the client)
            Mapper.CreateMap<Article, ArticleDto>();
            Mapper.CreateMap<ArticlesGroup, ArticlesGroupDto>();
            Mapper.CreateMap<Images, ImagesDto>();

            // API -> Inbound (Get data fro the client)
            Mapper.CreateMap<ArticleDto, Article>();
            Mapper.CreateMap<ArticlesGroupDto, ArticlesGroup>();
        }
    }
}