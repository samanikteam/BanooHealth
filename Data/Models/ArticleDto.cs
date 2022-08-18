using Entities.Articles;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ArticleDto
    {
        public int Id{ get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title { get; set; }

        [Required]
        [MaxLength(155)]
        [Display(Name = "توضیحات کوتاه یا متا")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "توضیحات جامع")]
        public string Text { get; set; }

        [Display(Name = "تصویر شاخص")]
        public byte[] Avatar { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان تصویر شاخص")]
        public string AvatarTitle { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "آلت تصویر شاخص")]
        public string AvatarAlt { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "اسلاگ")]
        public string Slug { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string KeyWords { get; set; }

        [Required]
        [Display(Name = "شناسه دسته بندی مجله")]
        public List<int> ArticleCategoryId { get; set; }
        public string ArticleCategoryTitle { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public string RegisterDate { get; set; }

        public long? Visit { get; set; }

        [Required]
        public bool IsDelete { get; set; }



        //لیست مجلات مرتبط
        public List<Article> ListArticleLinked { get; set; }
        //لیست محصولات مرتبط
        public List<Product> ListProductLinked { get; set; }

    }

    public class ListArticleDto
    {
        public List<ArticleDto> Articles { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int count { get; set; }
        public int skip { get; set; }
    }
}
