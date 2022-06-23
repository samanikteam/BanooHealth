using Entities.pharmacies;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Pharmacies
{
    public class PharmacyProducts : BaseEntity<int>
    {
        [Display(Name = "شناسه داروخانه")]
        public int PharmacyId { get; set; }
        [Display(Name ="شناسه محصول")]
        public int productId { get; set; }
        [Display(Name = "تعداد موجودی")]
        public int Inventory { get; set; }
        [Display(Name = "قیمت محصول")]
        public long Price { get; set; }
        [Display(Name = "اولویت قرارگیری در صفحه")]//0=very high / 1=high / 2=default
        public int SortId { get; set; }
        [Display(Name ="تخفیف محصول")] //بر اساس درصد
        public int Discount { get; set; }
        [Required]
        [Display(Name = "تاریخ ایجاد")]
        public DateTime RegisterDate { get; set; }
        [Required]
        [MaxLength(450)]
        [Display(Name = "ایجاد کننده")]
        public string RegisterUserId { get; set; }
        [Display(Name = "تاریخ ویرایش")]
        public DateTime? LastUpdateDate { get; set; }
        [MaxLength(450)]
        [Display(Name = "ویرایش کننده")]
        public string LastUpdateUserId { get; set; }

        [Display(Name = "لینک سایت داروخانه")]
        public string LinkAddress { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        #region Relation
        public Product Product { get; set; }
        public Pharmacy Pharmacy { get; set; }
        #endregion
    }
}
