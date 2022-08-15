using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class FilterDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public bool Status { get; set; }

    }
    public class ListFilterDto
    {
        public List<FilterDto> filters { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int count { get; set; }
        public int skip { get; set; }
    }

}
