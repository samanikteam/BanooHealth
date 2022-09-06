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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly IUserRepasitory _repasitory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;

        public IndexModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext , IUserRepasitory repasitory, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _repasitory = repasitory;
            _authorizationService = authorizationService;
        }
       
        [BindProperty]
        public List<ListUserDto> dto { get; set; }
        [BindProperty]
        public CreateUserDto createDto { get; set; }
        public EditUserDto editDto { get; set; }
        public IActionResult OnGet()
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Users).Result.Succeeded)
            {
                dto = _repasitory.GetAllUserInfo();
                ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
          
        }
        
        public async Task<IActionResult> OnPost(List<string> RoleId)
        {
            dto = _repasitory.GetAllUserInfo();
            ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");

            if (ModelState.IsValid)
            {
                //_repasitory.Addusers(createDto);
                var user = new ApplicationUser()
                {
                    UserName = createDto.UserName,
                    FirstName = createDto.FirstName,
                    LastName = createDto.LastName,
                    NationalCode = createDto.NationalCode,
                    Tel = createDto.Tel,
                };
                var resultUser = await _userManager.CreateAsync(user, createDto.Password);
                var userId = await _userManager.GetUserIdAsync(user);
                if (resultUser.Succeeded)
                {
                    #region Add Roles in Table UserRoles
                    foreach (var item in RoleId)
                    {
                        _dbContext.UserRoles.Add(new IdentityUserRole<string>
                        {
                            RoleId = item,
                            UserId = userId
                        });
                    }
                    _dbContext.SaveChanges();

                    //*******************End*********************
                    #endregion

                    return RedirectToPage("Index");

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
        
        public IActionResult OnPostDeleteUser(string userId ,  CancellationToken cancellationToken)
        {
            ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");
             _repasitory.Deactive(userId, cancellationToken);

            return Redirect("/Administration/Users/Index");
        }

        public IActionResult OnPostEnableUser(string userId, CancellationToken cancellationToken)
        {
            ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");

            _repasitory.Active(userId, cancellationToken);

            return Redirect("/Administration/Users/Index");
        }

        //public JsonResult OnGetGetUser(string userId)
        //{
        //    editDto = new EditUserDto();
        //   editDto = _repasitory.GetUserInfoById(userId);

        //    return new JsonResult(editDto);
        //}

        //public IActionResult OnPostEditUser(string userId, CancellationToken cancellationToken)
        //{
        //    ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");

        //    _repasitory.UpdateUser(userId , createDto);

        //    return Redirect("/Administration/Users/Index");
        //}

    }
}
