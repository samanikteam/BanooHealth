using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ArticleCategoryAssignDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "آی دی مجله")]
        public int ArticleId { get; set; }

        [Required]
        [Display(Name = "آی دی  دسته‌بندی")]
        public int CategoryId { get; set; }

        public int? SortNum { get; set; }

    }
    public class ListArticleCategoryAssignDto
    {
        public List<ArticleCategoryAssignDto> articleCategoryAssigns { get; set; }
    }
}
