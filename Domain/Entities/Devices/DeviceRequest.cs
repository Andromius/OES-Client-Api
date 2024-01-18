using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Devices;

public class DeviceRequest
{
    public string Name { get; set; }
    public bool IsWeb { get; set; }
    public EDevicePlatform PlatformId { get; set; }
}
