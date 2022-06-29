using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Media
{
    public class Banner : BaseEntity<int>
    {
        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title1 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar1 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link1 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title2{ get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar2 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link2 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title3 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar3 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link3 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title4 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar4 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link4 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title5 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar5 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link5 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title6 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar6 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link6 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title7 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar7 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link7 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title8 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar8 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link8 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title9 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar9 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link9 { get; set; }



        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title10 { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar10 { get; set; }
        [Required]
        [Display(Name = "لینک بنر")]
        public string Link10 { get; set; }
    }
}
