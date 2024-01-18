using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Sessions;

public static class SessionExtensions
{
    public static SessionResponse ToResponse(this Session s, DateTime lastSingInUtc)
    {
        return new SessionResponse(
            s.DeviceName,
            s.IsWeb,
            s.Token,
            s.DevicePlatformId,
            lastSingInUtc);
    }
}
