using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;

public static class UserExtensions
{
    public static UserResponse ToResponse(this User u)
    {
        return new UserResponse(u.Id, u.FirstName, u.LastName, u.Username, u.Role);
    }
}
