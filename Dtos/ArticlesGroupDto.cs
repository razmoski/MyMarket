using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellMyStuff.Dtos
{
    public class ArticlesGroupDto
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
    }
}