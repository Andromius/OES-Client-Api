using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes;
public record NoteResponse(string Type, int Id, string Name, DateTime Created, int CreatedById, bool IsVisible, string Data)
    : CourseItemResponse(Type, Id, Name, Created, CreatedById, IsVisible);
