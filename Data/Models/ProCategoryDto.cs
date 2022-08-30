using Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProCategoryDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "آی دی  محصول")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "آی دی  مجله")]
        public int ProductCategoryId { get; set; }
        public string Title { get; set; }

        public int? SortNum { get; set; }
    }

    public class ListProCategoryDto
    {
        public List<ProCategoryDto> ProCategories { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
