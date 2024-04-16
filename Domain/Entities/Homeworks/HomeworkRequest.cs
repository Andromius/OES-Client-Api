using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public record HomeworkRequest(
    string Name,
    DateTime Created,
    bool IsVisible,
    int CreatedById,
    DateTime Scheduled,
    DateTime End,
    string Task);
