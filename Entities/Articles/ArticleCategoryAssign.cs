using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Articles
{
    public class ArticleCategoryAssign : BaseEntity<int>
    {
        [Required]
        [Display(Name = "آی دی مجله")]
        public int ArticleId { get; set; }

        [Required]
        [Display(Name = "آی دی  دسته‌بندی")]
        public int CategoryId { get; set; }

        public int? SortNum { get; set; }

        #region Relation
        public Article article { get; set; }
        public Category category { get; set; }
        #endregion
    }
}
