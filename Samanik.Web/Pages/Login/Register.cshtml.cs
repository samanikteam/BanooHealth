using Data;
using Data.Models;
using Entities.Users;
using IPE.SmsIrRestful.TPL.NetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public RegisterModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        [BindProperty]
        public RegisterDto dto { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var codeConfirm = CreateRandomCodeSixDigits();
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = dto.UserName,
                    FirstName = "کاربر عادی",
                    LastName = "کاربر عادی",
                    NationalCode = "بدون کد ملی",
                    Tel = dto.UserName,
                };
                var resultUser = await _userManager.CreateAsync(user, dto.Password);
                var userId = await _userManager.GetUserIdAsync(user);



                if (resultUser.Succeeded)
                {
                    #region Add Roles in Table UserRoles

                    _dbContext.UserRoles.Add(new IdentityUserRole<string>
                    {
                        RoleId = "123",
                        UserId = userId
                    });

                    _dbContext.SaveChanges();

                    //*******************End*********************
                    #endregion

                    #region Send Sms

                    //sms
                    var token = new Token().GetToken("4eb0d6881702954f118fb0c5", "SamanikTeam2021");

                    var restVerificationCode = new RestVerificationCode()
                    {
                        Code = codeConfirm,
                        MobileNumber = dto.UserName

                    };
                    var restVerificationCodeRespone = new VerificationCode().Send(token, restVerificationCode);

                    if (restVerificationCodeRespone.IsSuccessful)
                    {
                        var getuser = _dbContext.Users.Where(_ => _.Id == userId).SingleOrDefault();
                        getuser.ConfirmCode = codeConfirm;
                        var updateResult = await _userManager.UpdateAsync(getuser);
                        if (updateResult.Succeeded)
                        {
                            return Redirect("/Login/VerifyPhoneNumber/" + dto.UserName);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "سرویس پیامک با خطا مواجه شد لطفا مجددا سعی نمایید");
                        return RedirectToPage("/Login/Register");

                    }
                    //end sms

                    #endregion

                    // return RedirectToPage("/Login/loginApp");

                }
                else
                {
                    foreach (var err in resultUser.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
            }
            return Page();
        }

        //ساخت عدد رندوم 6 رقمی برای ارسال اس ام اس
        public string CreateRandomCodeSixDigits()
        {

            Random rnd = new Random(); //نمونه سازی کلاس randome
            int a = rnd.Next(10, 29); //تعیین بازه برای تولید عدد تصادفی
            int b = rnd.Next(31, 58); //تعیین بازه برای تولید عدد تصادفی
            int c = rnd.Next(68, 84); //تعیین بازه برای تولید عدد تصادفی

            var rand = a + "" + b + "" + c;
            return rand;
        }
    }
}
