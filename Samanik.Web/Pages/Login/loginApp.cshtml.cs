using Data.Contracts;
using Data.Models;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Samanik.Web.Pages.Login
{
    [AllowAnonymous]
    public class loginAppModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoginSlider _sliderRepository;

        public loginAppModel(ILogger<IndexModel> logger,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILoginSlider sliderRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _sliderRepository = sliderRepository;
        }

        [BindProperty]
        public LoginDto dto { get; set; }
        public ListLoginSliderDto sliderDto { get; set; }

        public void OnGet()
        {
            //sliderDto = _sliderRepository.GetListLoginSliderDto();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RemmemberMe, true);
                var user = await _userManager.FindByNameAsync(dto.UserName);
                if (result.Succeeded && user.IsDeleted != true)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    await _signInManager.SignInWithClaimsAsync(user, dto.RemmemberMe, new List<Claim>()
                    {
                        new Claim("UserId" , user.Id)
                    });

                    return Redirect("/administration");
                }

                if (result.IsLockedOut)
                {
                    //ViewData["ErrorMessage"] = "اکانت شما به دلیل سه بار ورود ناموفق به مدت یک ساعت قفل شده است";
                    ModelState.AddModelError("", "اکانت شما به دلیل سه بار ورود ناموفق به مدت یک ساعت قفل شده است");
                    return Page();
                }
                ModelState.AddModelError("", "رمز عبور یا نام کاربری اشتباه است");
            }
            return Page();
        }
    }
}
