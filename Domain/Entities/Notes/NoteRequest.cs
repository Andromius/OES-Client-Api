using Domain.Entities.Courses;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes;
public class NoteRequest
{
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public int CreatedById { get; set; }
    public bool IsVisible { get; set; }
    public string Data { get; set; }
}
