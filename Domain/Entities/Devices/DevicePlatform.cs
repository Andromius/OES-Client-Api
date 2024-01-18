using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Devices;

public class DevicePlatform
{
    public EDevicePlatform Id { get; set; }
    public string PlatformName { get; set; }
    public DevicePlatform(EDevicePlatform id, string platformName)
    {
        Id = id;
        PlatformName = platformName;
    }

#pragma warning disable CS8618
    private DevicePlatform() { }
#pragma warning restore CS8618
}
