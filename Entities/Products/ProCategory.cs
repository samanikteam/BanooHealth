using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Products
{
    public class ProCategory : BaseEntity<int>
    {
        [Required]
        [Display(Name = "آی دی  محصول")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "آی دی  مجله")]
        public int ProductCategoryId { get; set; }
        public int? SortNum{ get; set; }

        #region Relation
        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        #endregion

    }
}
