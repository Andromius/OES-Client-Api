using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CentralConfig;
public record CentralConfigRequest(string Name, string Uri);
public record CentralConfigPasswordRequest(string Uri, string Password);
