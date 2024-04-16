using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;
public record UserQuizResultResponse(int Id, string FirstName, string LastName, string Username, int Points)
    : UserResponse(Id, FirstName, LastName, Username);
