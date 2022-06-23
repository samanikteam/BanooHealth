using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{
    public class Slogan : BaseEntity<int>
    {
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
        [Required]
        public bool IsActive { get; set; }
        
        public DateTime RegisterDate { get; set; }
    }
}
