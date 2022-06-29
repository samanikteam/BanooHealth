using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string NationalCode { get; set; }
        [Display(Name = "شماره تماس")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string Tel { get; set; }
        [Display(Name = "کد تایید")]
        [MaxLength(6, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string ConfirmCode { get; set; }
        public bool IsDeleted { get; set; }
    }
}
