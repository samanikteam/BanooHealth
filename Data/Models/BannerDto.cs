using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class BannerDto
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title1 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar1 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link1 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title2 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar2 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link2 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title3 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar3 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link3 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title4 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar4 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link4 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title5 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar5 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link5 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title6 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar6 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link6 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title7 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar7 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link7 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title8 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar8 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link8 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title9 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar9 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link9 { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title10 { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile Avatar10 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link10 { get; set; }


        public string AvatarShow1 { get; set; }
        public string AvatarShow2 { get; set; }
        public string AvatarShow3 { get; set; }
        public string AvatarShow4 { get; set; }
        public string AvatarShow5 { get; set; }
        public string AvatarShow6 { get; set; }
        public string AvatarShow7 { get; set; }
        public string AvatarShow8 { get; set; }
        public string AvatarShow9 { get; set; }
        public string AvatarShow10 { get; set; }
    }

    public class ListBannerDto
    {
        public List<BannerDto> banners { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
