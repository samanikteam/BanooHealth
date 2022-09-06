using Constants;
using Data.Models;
using Data.Models.Constants;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users.Roles
{
    public class PermissionModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthorizationService _authorizationService;

        public PermissionModel(RoleManager<IdentityRole> roleManager, IAuthorizationService authorizationService)
        {
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public PermissionViewModel model { get; set; }
        public async Task<ActionResult> OnGet(string id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Users).Result.Succeeded)
            {
                model = new PermissionViewModel();
                var allPermissions = new List<RoleClaimsViewModel>();
                allPermissions.GetPermissions(typeof(Permissions.Samanik), id);
                var role = await _roleManager.FindByIdAsync(id);
                model.RoleId = id;
                var claims = await _roleManager.GetClaimsAsync(role);
                var allClaimValues = allPermissions.Select(a => a.Value).ToList();
                var roleClaimValues = claims.Select(a => a.Value).ToList();
                var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
                foreach (var permission in allPermissions)
                {
                    if (authorizedClaims.Any(a => a == permission.Value))
                    {
                        permission.Selected = true;
                    }
                }
                model.RoleClaims = allPermissions;

                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }

        }

        public async Task<IActionResult> OnPost()
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            return RedirectToPage("Index");
        }
    }
}
