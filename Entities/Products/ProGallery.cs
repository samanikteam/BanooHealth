using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Products
{
    public class ProGallery : BaseEntity<long>
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "تصویر شاخص")]
        public byte[] Avatar { get; set; }


        [MaxLength(200)]
        [Display(Name = "عنوان آیکون")]
        public string AvatarTitle { get; set; }

        [MaxLength(200)]
        [Display(Name = "آلت آیکون")]
        public string AvatarAlt { get; set; }


        [Required]
        [Display(Name = "تاریخ ایجاد")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [MaxLength(450)]
        [Display(Name = "ایجاد کننده")]
        public string RegisterUserId { get; set; }


        [Required]
        public bool IsDelete { get; set; }

        #region Relation
        public Product Product { get; set; }
        #endregion
    }
}
