using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Courses;

public record CourseItemResponse(string Type, int Id, string Name, DateTime Created, int CreatedById, bool IsVisible);
