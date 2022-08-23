using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{
    public class About : BaseEntity<int>
    {
        [Display(Name = "تصویر شاخص")]
        public byte[] Avatar { get; set; }
        [Required]
        public string Body { get; set; }


        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان تصویر شاخص")]
        public string AvatarTitle { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "آلت تصویر شاخص")]
        public string AvatarAlt { get; set; }
    }
}
