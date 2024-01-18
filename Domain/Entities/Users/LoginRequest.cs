using Domain.Entities.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public DeviceRequest DeviceRequest { get; set; }
}
