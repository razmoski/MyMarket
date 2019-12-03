using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellMyStuff.Models
{
    public class Images
    {
        public int Id { get; set; }

        public int Sequence { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string SrcPath { get; set; }

        public string FileName { get; set; }

        public int ArticleId { get; set; }
    
    }


}