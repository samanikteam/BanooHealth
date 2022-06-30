using System.Collections.Generic;

namespace Data.Models
{
    public class EditUserDto:CreateUserDto
    {
        public List<string> RoleId { get; set; }

    }
}
