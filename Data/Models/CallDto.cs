using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CallDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "پیام")]
        public string Message { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "وضعیت")]
        public bool Status { get; set; }

        public string date { get; set; }
    }

    public class ListCallDto
    {
        public List<CallDto> calls { get; set; }
    }
}
