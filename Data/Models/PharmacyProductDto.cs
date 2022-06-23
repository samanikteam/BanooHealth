using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PharmacyProductDto
    {
        public int Id { get; set; }

        [Display(Name = "شناسه داروخانه")]
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        [Display(Name = "شناسه محصول")]
        public int productId { get; set; }
        public string ProductName { get; set; }
        [Display(Name = "تعداد موجودی")]
        public int Inventory { get; set; }
        [Display(Name = "قیمت محصول")]
        public long Price { get; set; }
        [Display(Name = "اولویت قرارگیری در صفحه")]//0=very high / 1=high / 2=default
        public int SortId { get; set; }
        [Display(Name = "تخفیف محصول")] //بر اساس درصد
        public int Discount { get; set; }

        [Display(Name = "لینک سایت داروخانه")]
        public string LinkAddress { get; set; }

        #region Four property All

        [Display(Name = "تاریخ ایجاد")]
        public DateTime RegisterDate { get; set; }

        [MaxLength(450)]
        [Display(Name = "ایجاد کننده")]
        public string RegisterUserId { get; set; }
        [Display(Name = "تاریخ ویرایش")]
        public DateTime? LastUpdateDate { get; set; }
        [MaxLength(450)]
        [Display(Name = "ویرایش کننده")]
        public string LastUpdateUserId { get; set; }


        public bool IsDelete { get; set; }
        #endregion

    }
    public class ListPharmacyProductDto
    {
        public List<PharmacyProductDto> PharmacyProducts { get; set; }
    }
}
