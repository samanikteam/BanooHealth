using Data;
using Data.Contracts;
using Data.Models;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Samanik.Web.Areas.Administration.Pages.Users
{
    public class EditUserModel : PageModel
    {
        private readonly IUserRepasitory _repasitory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public EditUserModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext, IUserRepasitory repasitory)
        {
            _userManager = userManager;
            _roleManager = roleManager;


            _dbContext = dbContext;
            _repasitory = repasitory;
        }

        [BindProperty]
        public EditUserDto editDto { get; set; }

        public void OnGet(string id)
        {
            ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");

            editDto = _repasitory.GetUserInfoById(id);
        }

        public IActionResult OnPost(List<string> RoleId , CancellationToken cancellationToken)
        {

            #region رکورد قبلی رو حذف میکنیم
            var userIdForDelete = editDto.Id;
 
            _repasitory.Deactive(userIdForDelete, cancellationToken);
            #endregion


            #region یه رکورد جدید ثبت میکنیم
            var newUser = new ApplicationUser();
            newUser.UserName = editDto.UserName;
            newUser.FirstName = editDto.FirstName;
            newUser.LastName = editDto.LastName;
            newUser.NationalCode = editDto.NationalCode;
            newUser.Tel = editDto.Tel;
            var resultUser =  _userManager.CreateAsync(newUser, editDto.Password).Result;
            _dbContext.SaveChanges();

            var userId =  _userManager.GetUserIdAsync(newUser).Result;
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
            #endregion

            #region نقش های نفر رو هم پاک میکنیم
            //var roleList= _dbContext.UserRoles.Where(x=>x.UserId==userId).ToList();
            //foreach (var role in roleList)
            //{
            //    _dbContext.UserRoles.Remove(role);

            //}
            //_dbContext.SaveChanges();
            #endregion


            return Redirect("/administration/users");
        }
    }
}
