using Domain.Entities.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Sessions;

public class SessionResponse
{
    public SessionResponse(string name, bool isWeb, string deviceToken, EDevicePlatform platformId, DateTime lastSignIn)
    {
        Name = name;
        IsWeb = isWeb;
        DeviceToken = deviceToken;
        PlatformId = platformId;
        LastSignIn = lastSignIn;
    }

    public string Name { get; set; }
    public bool IsWeb { get; set; }
    public DateTime LastSignIn { get; set; }
    public string DeviceToken { get; set; }
    public EDevicePlatform PlatformId { get; set; }
}
