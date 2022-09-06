using Data;
using Data.Models.Constants;
using Data.Models;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGet(string Id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Users).Result.Succeeded)
            {
                var role = await _roleManager.FindByIdAsync(Id);
                var res = _roleManager.DeleteAsync(role).Result;
                if (res.Succeeded)
                    _dbContext.SaveChanges();
                return Redirect("/administration/users/roles/Index");
            }
            else
            {
                return Redirect("/login/logout");
            }

        }
    }
}
