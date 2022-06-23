using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.Login
{
    public class VerifyPhoneNumberModel : PageModel
    {
        private readonly IUserRepasitory _repasitory;
        public VerifyPhoneNumberModel(IUserRepasitory repasitory)
        {
            _repasitory = repasitory;
        }
        [BindProperty]
        public VerifyPhoneNumberDto dto { get; set; }
        public void OnGet(string userName)
        {
            dto = new VerifyPhoneNumberDto();
            dto.userName = userName;
        }
        public IActionResult OnPost(string ConfirmCode)
        {
            bool checkCode = _repasitory.CheckCode(ConfirmCode , dto.userName);
            if (checkCode == true)
            {
                return RedirectToPage("loginApp");
            }
            ModelState.AddModelError("", "کد وارد شده اشتباه است");
            return Page();
        }
    }
}
