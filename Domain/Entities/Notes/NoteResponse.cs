using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes;
public class NoteResponse : CourseItemResponse
{
    public string Data { get; set; }
    public NoteResponse(string type, int id, string name, DateTime created, int createdById, bool isVisible) : base(type, id, name, created, createdById, isVisible)
    {
        Type = type;
        Id = id;
        Name = name;
        Created = created;
        CreatedById = createdById;
        IsVisible = isVisible;
    }
}
