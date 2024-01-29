using Domain.Entities.Courses;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes;
public static class NoteExtensions
{
    public static NoteResponse ToResponse(this Note note)
    {
        return new NoteResponse(
            nameof(Note),
            note.Id,
            note.Name,
            note.Created,
            note.UserId,
            note.IsVisible)
        {
            Data = note.Data
        };
    }

    public static Note ToNote(this NoteRequest request, User user, Course course)
    {
        return new Note()
        {
            Course = course,
            CourseId = course.Id,
            Created = request.Created,
            Data = request.Data,
            IsVisible = request.IsVisible,
            Name = request.Name,
            UserId = user.Id,
            User = user
        };
    }
}
