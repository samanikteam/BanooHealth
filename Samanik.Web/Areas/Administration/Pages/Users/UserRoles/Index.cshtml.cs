using Data.Models;
using Data.Models.Constants;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users.UserRoles
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }
        //public List<UserRolesViewModel> viewModel { get; set; }
        [BindProperty]
        public ManageUserRolesViewModel model { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Users).Result.Succeeded)
            {
                var viewModel = new List<UserRolesViewModel>();
                var user = await _userManager.FindByIdAsync(id);
                foreach (var role in _roleManager.Roles.ToList())
                {
                    var userRolesViewModel = new UserRolesViewModel
                    {
                        RoleName = role.Name
                    };
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRolesViewModel.Selected = true;
                    }
                    else
                    {
                        userRolesViewModel.Selected = false;
                    }
                    viewModel.Add(userRolesViewModel);
                }
                model = new ManageUserRolesViewModel()
                {
                    UserId = id,
                    UserRoles = viewModel
                };

                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }

        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            //await Seeds.DefaultUsers.SeedSuperAdminAsync(_userManager, _roleManager);
            return Redirect("/Administration/users/Index");
        }
    }
}
