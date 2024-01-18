using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class CustomToken
{
    public int UserId { get; set; }
    public UserRole UserRole { get; set; }
    public DateTime Creation { get; set; }
    public DateTime Expiration { get; set; }
}
