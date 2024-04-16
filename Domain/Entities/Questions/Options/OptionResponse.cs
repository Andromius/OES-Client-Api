using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Questions.Options;

public record OptionResponse(int Id, string Text, int? Points);
