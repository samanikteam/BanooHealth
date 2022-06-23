using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SiteSettingDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "عنوان سایت")]
        public string Title { get; set; }

        [Required]
        [MaxLength(155)]
        [Display(Name = "عنوان سایت")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "کلمات کلیدی")]
        public string Keywords { get; set; }

        [Display(Name = "لوگو")]
        public IFormFile Logo { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "عنوان لوگو")]
        public string LogoTitle { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "متن جایگزین لوگو")]
        public string LogoAlt { get; set; }

        [Display(Name = "آیکون")]
        public IFormFile Favicon { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "عنوان آیکون")]
        public string FaviconTitle { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "متن جایگزین آیکون")]
        public string FaviconAlt { get; set; }

        [Required]
        [Display(Name = "تعداد نمایش لیست")]
        public int Paging { get; set; }

        [Required]
        [Display(Name = "SMTP")]
        public string SMTP { get; set; }


        public string FaviconShow { get; set; }
        public string LogoShow { get; set; }

    }
}
