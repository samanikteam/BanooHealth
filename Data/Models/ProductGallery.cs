using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
   public class ProductGallery
    {
        public long Id{ get; set; }

        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }


        [Display(Name = "شناسه گالری")]
        public long ProGalleryId { get; set; }

        [Display(Name = "سورت")]
        public int? sortNum { get; set; }
    }
}
