using Entities.Pharmacies;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.pharmacies
{
    public class Pharmacy : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "نام داروخانه")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "نام کاریری")]
        public string Username { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "نام دکتر داروخانه")]
        public string DrName { get; set; }

        [Required]
        [MaxLength(11)]
        [Display(Name = "شماره ثابت")]
        public string Phone { get; set; }


        [Required]
        [MaxLength(11)]
        [Display(Name = "شماره همراه")]
        public string Mobile { get; set; }

        [Required]
        [MaxLength(70)]
        [Display(Name = "کد اختصاصی  داروخانه")]
        public string PharmacyCode { get; set; }


        [Required]
        [MaxLength(20)]
        [Display(Name = "استان")]
        public string Province { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "شهر")]
        public string City { get; set; }


        [Required]
        [MaxLength(500)]
        [Display(Name = "آدرس داروخانه")]
        public string Address { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime Registerdate { get; set; }


        #region Relation
        public List<PharmacyProducts> PharmacyProducts { get; set; }
        #endregion
    }
}
