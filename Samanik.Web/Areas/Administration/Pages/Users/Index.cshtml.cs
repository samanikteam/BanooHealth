﻿using Data;
using Data.Contracts;
using Data.Models;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Users
{
    //[Authorize(Roles ="Admin")]
    [AutoValidateAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly IUserRepasitory _repasitory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext , IUserRepasitory repasitory)
        {
            _userManager = userManager;
            _roleManager = roleManager;


            _dbContext = dbContext;
            _repasitory = repasitory;
        }
       
        [BindProperty]
        public List<ListUserDto> dto { get; set; }
        [BindProperty]
        public CreateUserDto createDto { get; set; }
        public void OnGet()
        {
            dto = _repasitory.GetAllUserInfo();
            ViewData["Roles"] = new SelectList(_repasitory.GetRoles(), "Id", "Name");
        }
        
        public async Task<IActionResult> OnPost(List<string> RoleId)
        {
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
                //_repasitory.UpdateUser(userId ,createDto);
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
        
        public void OnPostDeleteUser(string Userid)
        {

        }

    }
}
