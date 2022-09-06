using Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Articles
{
    public class Comment: BaseEntity<int>
    {
        public int? ParentId { get; set; }

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
        public DateTime RegisterDate { get; set; }

        public int ArticleId { get; set; }

        #region Relations
        public Article Article { get; set; }

        [ForeignKey("ParentId")]
        public List<Comment> Comments { get; set; }
        #endregion


        public void Confirm()
        {
            Status = Statuses.Confirm;
        }

        public void Cancel()
        {
            Status = Statuses.Cancel;
        }
    }
}
