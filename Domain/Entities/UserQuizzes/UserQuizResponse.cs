using Domain.Entities.Courses;
using Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserQuizzes;
public record UserQuizResponse(string Type, int Id, string Name, DateTime Created, int CreatedById, bool IsVisible, bool ShouldShuffleQuestions, List<QuestionResponse> Questions)
    : CourseItemResponse(Type, Id, Name, Created, CreatedById, IsVisible);
