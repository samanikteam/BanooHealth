using Data.Models;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Samanik.Web.Pages.Login
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ForgetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public ForgetPasswordDto dto { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.FindByNameAsync(dto.userName);
            if (user != null)
            {

            }
            ModelState.AddModelError("", "نام کاربری یا شماره موبایل ثبت نام نکرده است");
            return Page();
        }
    }
}
