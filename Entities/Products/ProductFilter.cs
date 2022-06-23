using Entities.Articles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Products
{
    public class ProductFilter : BaseEntity<int>
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int FilterId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Value { get; set; }

        [Required]
        public bool Status { get; set; }

        #region Relations
        public Product product { get; set; }
        public Filter filter { get; set; }
        #endregion


        public void Confirm()
        {
            Status = true;
        }

        public void Cancel()
        {
            Status = false;
        }


    }
}
