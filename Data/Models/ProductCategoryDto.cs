using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "نام دسته")]
        public string Title { get; set; }

        [MaxLength(155)]
        [Display(Name = "توضیحات دسته")]
        public string Description { get; set; }


        [Display(Name = "آیکون دسته")]
        public byte[] Avatar { get; set; }

        [MaxLength(200)]
        [Display(Name = "عنوان آیکون شاخص")]
        public string AvatarTitle { get; set; }

        [MaxLength(200)]
        [Display(Name = "آلت آیکون شاخص")]
        public string AvatarAlt { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "اسلاگ")]
        public string Slug { get; set; }

        [Display(Name = "شناسه والد")]
        public int? ParentId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime RegisterDate { get; set; }
        public string RegisterDateFa { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public string Mothername { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string Keywords { get; set; }
    }

    public class ListProductCategoryDto
    {
        public List<ProductCategoryDto> ProductCategories { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
