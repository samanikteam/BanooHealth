using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AboutDto
    {
        [Required]
        [Display(Name = "تصویر شاخص")]
        public byte[] Avatar { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
