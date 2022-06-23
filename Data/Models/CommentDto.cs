using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "نظر")]
        public string Message { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [Display(Name = "تاریخ ایجاد")]
        public string RegisterDate { get; set; }

        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }

    }

    public class ListCommentDto
    {
        public List<CommentDto> Comments { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

  

}
