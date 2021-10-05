using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOIC_ASU.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="اسم الفئة")]
        public string CAtegoryName { get; set; }
        [Required]
        [Display(Name = "وصف الفئة")]
        public string CategoryDescription { get; set; }
        public virtual ICollection<Articale> Articales { get; set; }
        public virtual ICollection<TempArticale> TempArticales { get; set; }
    }
}