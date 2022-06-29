using Data.Contracts;
using Data.Models;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepasitory :IUserRepasitory
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepasitory(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public void Addusers(CreateUserDto createDto)
        {
            
            var result = new ApplicationUser()
            {
                UserName = createDto.UserName,      
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                NationalCode = createDto.NationalCode,  
                Tel = createDto.Tel,
                
            };
        }

        public bool CheckCode(string confirmCode, string userName)
        {
            var user = _context.Users.Where(_ => _.UserName == userName).SingleOrDefault();
            if (user.ConfirmCode == confirmCode)
            {
                return true;
            }
            return false;
        }



        public List<ListUserDto> GetAllUserInfo()
        {
            var result = _context.Users.ToList();
            return result.Select(_ => new ListUserDto()
            {
                Id = _.Id,
                FirstName = _.FirstName,
                LastName = _.LastName,
                NationalCode = _.NationalCode,
                Tel = _.Tel,
                IsDeleted = _.IsDeleted,
            }).OrderByDescending(x=>x.Id).ToList();
        }

        public IEnumerable GetRoles()
        {
           return _context.Roles.ToList();
        }

        public EditUserDto GetUserInfoById(string Id)
        {
            var roles = _context.UserRoles.Where(t => t.UserId == Id).Select(t => t.RoleId).ToList();
            return _context.Users.Where(t => t.Id == Id).Select(t => new EditUserDto()
            {
                //UserId = t.Id,
                //FullName = t.NormalizedUserName,
                //UserName = t.UserName,
                //UserRoles = roles,
            }).SingleOrDefault();
        }

        public void UpdateUser(string userId, CreateUserDto createDto)
        {
            var findUser = _context.Users.Where(_ => _.Id == userId);
            var result = new ApplicationUser()
            {
                Id = userId,
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                NationalCode = createDto.NationalCode,
                Tel = createDto.Tel,
            };
            _context.Users.Update(result);
        }


        public void Deactive(string id, CancellationToken cancellationToken)
        {
            var user = _context.Users.Find(id);
            user.IsDeleted = true;
             _context.SaveChanges();
        }

        public void Active(string id, CancellationToken cancellationToken)
        {
            var user = _context.Users.Find(id);
            user.IsDeleted = false;
            _context.SaveChanges();
        }
    }
}
