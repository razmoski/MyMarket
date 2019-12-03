using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellMyStuff.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Number in Stock")]
        public byte StockQty { get; set; }

        public ArticlesGroup ArticlesGroup { get; set; }

        public string Colour { get; set; }
        public double Price { get; set; }
                
        [Required]
        [Display(Name = "Group")]
        public int ArticlesGroupId { get; set; }

        [JsonIgnore]
        public ICollection<Images> Images { get; set; }


    }
}