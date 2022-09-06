using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users.Roles
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthorizationService _authorizationService;

        public IndexModel(RoleManager<IdentityRole> roleManager, IAuthorizationService authorizationService)
        {
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public List<IdentityRole> dto { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Users).Result.Succeeded)
            {
                dto = await _roleManager.Roles.ToListAsync();
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
        }

        public async Task<IActionResult> OnPost(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToPage("Index");
        }
    }
}
