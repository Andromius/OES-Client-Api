using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Questions.Options;
public static class OptionExtensions
{
    public static OptionResponse ToResponse(this Option option, CourseEnum courseParticipant, bool returnPointsAnyway) =>
        new(option.Id, option.Text, courseParticipant is CourseEnum.Teacher || returnPointsAnyway ? option.Points : null);

    public static Option ToOption(this OptionRequest request, int id) => new(id, request.Text, request.Points);
}
