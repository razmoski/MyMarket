using SellMyStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellMyStuff.Dtos
{
    public class ImagesDto
    {
        public int Id { get; set; }

        public int Sequence { get; set; }
        
        public string FileName { get; set; }

        public int ArticleId { get; set; }       
    }
}