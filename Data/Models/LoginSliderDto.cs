using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class LoginSliderDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "نام مجله")]
        public string Title { get; set; }

        [Display(Name = "تصویر بنر")]
        public byte[] Avatar { get; set; }
        
        [Required]
        [MaxLength(200)]
        [Display(Name = "متن جایگزین")]
        public string AvatarAlt { get; set; }

        [Required]
        [Display(Name = "وضعیت بنر")]
        public bool Status { get; set; }

        [Display(Name = "ترتیب بنر")]
        public int Sort { get; set; }
    }

    public class ListLoginSliderDto
    {
        public List<LoginSliderDto> LoginSliders { get; set; }
    }
}
