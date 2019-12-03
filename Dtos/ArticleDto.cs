using SellMyStuff.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellMyStuff.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public byte StockQty { get; set; }

        public int ArticlesGroupId { get; set; }

        public string Colour { get; set; }
        public double? Price { get; set; }

        public ArticlesGroupDto ArticlesGroup { get; set; }

        public ICollection<ImagesDto> Images { get; set; }

    }
}