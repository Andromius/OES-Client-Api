using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CentralConfig;
public class CentralConfig
{
    public int Id { get; set; }
    public Uri AppAddress { get; set; }
    public string AppName { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
}
