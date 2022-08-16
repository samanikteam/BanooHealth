using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ContactusDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "شماره تماس")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
