using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProGalleryDto
    {


        public long Id { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "تصویر شاخص")]
        public byte[] Avatar { get; set; }


        [Display(Name = "عنوان آیکون")]
        public string AvatarTitle { get; set; }


        [Display(Name = "آلت آیکون")]
        public string AvatarAlt { get; set; }



        [Display(Name = "تاریخ ایجاد")]
        public DateTime RegisterDate { get; set; }


        [Display(Name = "ایجاد کننده")]
        public string RegisterUserId { get; set; }


        public bool IsDelete { get; set; }
    }

    public class ListProGalleryDto
    {
        public List<ProGalleryDto> ProGallery { get; set; }

    }
}
