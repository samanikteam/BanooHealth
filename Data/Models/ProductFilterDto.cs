using Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductFilterDto
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int FilterId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Value { get; set; }

        public bool Status { get; set; }

        public string Filtername { get; set; }

    }

    public class ListProductFilterDto
    {
        public List<ProductFilterDto> ProductFilters { get; set; }
    }

}
