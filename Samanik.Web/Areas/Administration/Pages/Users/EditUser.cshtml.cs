using Data;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users
{
    [Authorize]
    public class EditUserModel : PageModel
    {
        private readonly IUserRepasitory _repasitory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;

        public EditUserModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext, IUserRepasitory repasitory, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;


            _dbContext = dbContext;
            _repasitory = repasitory;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public EditUserDto editDto { get; set; }

        public IActionResult OnGet(string id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Users).Result.Succeeded)
            {
                ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");
                editDto = _repasitory.GetUserInfoById(id);
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
            
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {

            #region یه رکورد جدید ثبت میکنیم
            var newUser = await _userManager.FindByIdAsync(editDto.Id);
            if (editDto.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(newUser);

                var result = await _userManager.ResetPasswordAsync(newUser, token, editDto.Password);
            }
            newUser.UserName = editDto.UserName;
            newUser.FirstName = editDto.FirstName;
            newUser.LastName = editDto.LastName;
            newUser.NationalCode = editDto.NationalCode;
            newUser.Tel = editDto.Tel;
            var resultUser = _userManager.UpdateAsync(newUser).Result;
            _dbContext.SaveChanges();

            var userId = _userManager.GetUserIdAsync(newUser).Result;
            if (resultUser.Succeeded)
            {
                //#region Add Roles in Table UserRoles
                //foreach (var item in RoleId)
                //{
                //    _dbContext.UserRoles.Add(new IdentityUserRole<string>
                //    {
                //        RoleId = item,
                //        UserId = userId
                //    });
                //}
                //_dbContext.SaveChanges();

                ////*******************End*********************
                //#endregion

                return RedirectToPage("Index");

            }
            else
            {

                ModelState.AddModelError("", "در بروزرسانی کاربر مورد نظر خطایی به وجود آمده است");
                return Page();
            }
            #endregion

        }
    }
}
