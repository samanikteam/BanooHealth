using Entities.Pharmacies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Products
{
    public class Product : BaseEntity<int>
    {
        [Required]
        [MaxLength(150)]
        [Display(Name = "نام محصول")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "کشور سازنده")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "شناسه دسته بندی محصولات")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "تصویر شاخص")]
        public byte[] Avatar1 { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان تصویر شاخص")]
        public string AvatarTitle1 { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "آلت تصویر شاخص")]
        public string AvatarAlt1 { get; set; }


        [Required]
        [MaxLength(150)]
        [Display(Name = "اسلاگ")]
        public string Slug { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [MaxLength(200)]
        public string KeyWords { get; set; }

        [Display(Name = "کشور سازنده")]
        [MaxLength(50)]
        public string Country { get; set; }

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


        [MaxLength(100)]
        [Display(Name = "نام برند")]
        public string BrandName { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        public bool popular { get; set; }

        #region Relations
        public List<PharmacyProducts> PharmacyProducts { get; set; }
        public List<ProductArticle> ProductArticles { get; set; }
        public List<ProCategory> ProCategories { get; set; }
        public List<ProGallery> ProGalleries{ get; set; }
        //public List<ProComment> ProComments { get; set; }
        //public ProCategory ProCategory { get; set; }
        #endregion

        public void Active()
        {
            IsDelete = false;
        }

        public void Deactive()
        {
            IsDelete = true;
        }
    }
}
