using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Products
{
    public class Filter : BaseEntity<int>
    {
        [Required]
        public  string Title { get; set; }
        public bool Status { get; set; }

        

        public void Active()
        {
            Status = true;
        }
        public void Deactive()
        {
            Status = false;
        }
    }
}
