using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SellMyStuff.Models
{
    public class ArticlesGroup
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Image")]
        public byte[] Image { get; set; }

        [DisplayName("Image")]
        public string FileName { get; set; }
    }
}