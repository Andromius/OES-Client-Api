using Domain.Entities.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
public class DevicePlatformConfiguration : IEntityTypeConfiguration<DevicePlatform>
{
    public void Configure(EntityTypeBuilder<DevicePlatform> builder)
    {
        builder.HasData(SeedDevicePlatforms());
    }

    private static IEnumerable<DevicePlatform> SeedDevicePlatforms()
    {
        List<DevicePlatform> platforms = new();
        foreach (var platform in Enum.GetValues<EDevicePlatform>())
        {
            platforms.Add(new DevicePlatform(platform, platform.ToString()));
        }
        return platforms;
    }
}
