using Data.Models;
using Data.Repositories;
using Entities.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IUserRepasitory
    {
        EditUserDto GetUserInfoById(string Id);
        List<ListUserDto> GetAllUserInfo();
        void UpdateUser(string userId, EditUserDto editDto);
        IEnumerable GetRoles();
        void Addusers(CreateUserDto createDto);
        bool CheckCode(string confirmCode, string userName);

        void Deactive(string id, CancellationToken cancellationToken);
        void Active(string id, CancellationToken cancellationToken);

    }
}
