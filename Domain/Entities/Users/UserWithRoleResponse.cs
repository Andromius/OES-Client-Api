using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;

public record UserWithRoleResponse(int Id, string FirstName, string LastName, string Username, UserRole Role)
    : UserResponse(Id, FirstName, LastName, Username);
