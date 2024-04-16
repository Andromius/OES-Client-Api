using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;
public record UserResponse(int Id, string FirstName, string LastName, string Username);
