using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOIC_ASU.Models
{
    public class Articale
    {
        public int Id { get; set; }
  
        [Display(Name ="عنوان المقال")]
        public string ArticaleTitle { get; set; }
     
        [Display(Name = "صورة الغلاف")]
        public string ArticaleCover { get; set; }
     
        [Display(Name = "المحتوى")]
        [AllowHtml]
        public string ArticaleContent { get; set; }
     
        [Display(Name = "نوع المقال")]
        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }

        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}