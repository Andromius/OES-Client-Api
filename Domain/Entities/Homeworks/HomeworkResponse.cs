using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.Homeworks;
public record HomeworkResponse(string Type,
                               int Id,
                               string Name,
                               DateTime Created,
                               int CreatedById,
                               bool IsVisible,
                               string Task,
                               DateTime End,
                               DateTime Scheduled)
    : CourseItemResponse(Type, Id, Name, Created, CreatedById, IsVisible);
