using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;

public class UserResponse
{
    public UserResponse(int id, string firstName, string lastName, string username, UserRole role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Role = role;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public UserRole Role { get; set; }
}
