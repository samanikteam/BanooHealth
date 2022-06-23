using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SloganDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "آیکون")]
        public byte[] Avatar { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان آیکون")]
        public string AvatarTitle { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "آلت آیکون")]
        public string AvatarAlt { get; set; }

        public bool IsActive { get; set; }
        public string RegisterDate { get; set; }
    }

    public class ListSloganDto
    {
        public List<SloganDto> Slogans { get; set; }
    }
}
