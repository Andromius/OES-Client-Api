using Domain.Entities.Devices;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Sessions;

public class Session
{
    public Session(string deviceName, bool isWeb, string token, DevicePlatform devicePlatform, User user)
    {
        DeviceName = deviceName;
        IsWeb = isWeb;
        Token = token;
        DevicePlatformId = devicePlatform.Id;
        DevicePlatform = devicePlatform;
        UserId = user.Id;
        User = user;
    }

#pragma warning disable CS8618
    private Session() { }
#pragma warning restore CS8618
    public string DeviceName { get; set; }
    public bool IsWeb { get; set; }

    public string Token { get; set; }
    public EDevicePlatform DevicePlatformId { get; set; }
    public DevicePlatform DevicePlatform { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
