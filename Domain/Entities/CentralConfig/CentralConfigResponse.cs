using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CentralConfig;
public record CentralConfigResponse(string Name, Uri Uri, string Password);
