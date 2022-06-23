using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PharmacyDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "نام داروخانه")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "نام کاریری")]
        public string Username { get; set; }


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

        //حداقل قیمت محصول
        public long MinimumPrice { get; set; }
        //تعداد داروخانه هایی که محول رو موجود دارند
        public int CountPharmacyExistProduct { get; set; }
    }
    public class ListPharmacyDto
    {
        public List<PharmacyDto> pharmacies { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class PharmacyWithProductDto : PharmacyDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSlug { get; set; }

        public string ProductKeyWords { get; set; }

        public string ProductCountry { get; set; }

        public DateTime ProductRegisterDate { get; set; }

        public string ProductRegisterUserId { get; set; }

        public bool ProductIsDelete { get; set; }
        public long ProductPrice { get; set; }

        public string LinkAddress { get; set; }
    }
    public class ListPharmacyWithProductDto
    {
        public List<PharmacyWithProductDto> pharmaciesWithProduct { get; set; }
    }
}
