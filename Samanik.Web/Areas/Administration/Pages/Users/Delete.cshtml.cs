using Data;
using Data.Models.Constants;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        public DeleteModel(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGet(string Id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Users).Result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(Id);
                user.IsDeleted = true;
                var res = _userManager.UpdateAsync(user).Result;
                if (res.Succeeded)
                    _dbContext.SaveChanges();
                return Redirect("/administration/users/Index");
            }
            else
            {
                return Redirect("/login/logout");
            }
        }
    }
}
