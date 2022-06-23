using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Media
{
    public class Slider : BaseEntity<int>
    {
        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title { get; set; }

        [Display(Name = "تصویر بنر")]
        public string Avatar { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "متن جایگزین")]
        public string AvatarAlt { get; set; }

        [Required]
        [Display(Name = "لینک بنر")]
        public string Link { get; set; }

        [Required]
        [Display(Name = "وضعیت بنر")]
        public bool Status { get; set; }

        [Display(Name = "ترتیب بنر")]
        public int Sort { get; set; }


        public void Enable()
        {
            Status = true;
        }

        public void Disable()
        {
            Status = false;
        }
    }
}
