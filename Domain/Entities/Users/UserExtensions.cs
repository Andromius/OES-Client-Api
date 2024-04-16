using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;

public static class UserExtensions
{
    public static UserWithRoleResponse ToResponse(this User u)
    {
        return new UserWithRoleResponse(u.Id, u.FirstName, u.LastName, u.Username, u.Role);
    }
}
