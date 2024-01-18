using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Questions;
public static class OptionExtensions
{
    public static OptionResponse ToResponse(this Option option, CourseEnum who)
    {
        return who switch
        {
            CourseEnum.Teacher => new(option.Id, option.Text, option.Points),
            CourseEnum.Attendant => new(option.Id, option.Text, null),
            _ => throw new NotImplementedException()
        };
    }

    public static Option ToOption(this OptionRequest request, int id) => new(id, request.Text, request.Points);
}
