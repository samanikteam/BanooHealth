using Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Articles
{
    public class Article : BaseEntity<int>
    {
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

        [Required]
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
        [Display(Name = "تاریخ ایجاد")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [MaxLength(450)]
        [Display(Name = "ایجاد کننده")]
        public string RegisterUserId { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? LastUpdateDate { get; set; }

        [MaxLength(450)]
        [Display(Name = "ویرایش کننده")]
        public string LastUpdateUserId { get; set; }

        [Required]
        [Display(Name = "شناسه دسته بندی مجله")]
        public int ArticleCategoryId { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public long? Visit { get; set; }

        [Display(Name = "نویسنده")]
        public string author { get; set; }

        #region Relations
        public Category Category { get; set; }
        public List<ProductArticle> ProductArticles { get; set; }
        #endregion


        public void Visited()
        {
            Visit++;
        }
    }
}
