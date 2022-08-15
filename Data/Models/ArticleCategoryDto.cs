using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ArticleCategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "نام دسته")]
        public string Title { get; set; }

        [MaxLength(155)]
        [Display(Name = "توضیحات دسته")]
        public string Description { get; set; }

        [Display(Name = "تصویر شاخص")]
        public byte[] Avatar { get; set; }

        [MaxLength(200)]
        [Display(Name = "عنوان آیکون")]
        public string AvatarTitle { get; set; }

        [MaxLength(200)]
        [Display(Name = "آلت آیکون")]
        public string AvatarAlt { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "اسلاگ")]
        public string Slug { get; set; }

        [Display(Name = "شناسه والد")]
        public int? ParentId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public string RegisterDate { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [Required]
        [Display(Name = "کلمات کلیدی")]
        public string keywords { get; set; }
    }

    public class ListArticleCategoryDto
    {
        public List<ArticleCategoryDto> articleCategories { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int count { get; set; }
        public int skip { get; set; }
    }
}
