using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;

public class UserAuthResponse
{
    public UserAuthResponse(int id, string firstName, string lastName, string username, string token, UserRole role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Token = token;
        Role = role;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
    public UserRole Role { get; set; }
}
