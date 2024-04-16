using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserQuizzes;
public static class UserQuizExtensions
{
    public static UserQuizResponse ToResponse(this UserQuiz quiz, CourseEnum courseParticipant) => new(
        nameof(UserQuizResponse),
        quiz.Id,
        quiz.Name,
        quiz.Created,
        quiz.UserId,
        quiz.IsVisible,
        quiz.Questions.Select(q => q.ToResponse(courseParticipant, true)).ToList());

    public static UserQuiz ToUserQuiz(this UserQuizRequest request, int courseId, int userId) => new()
    {
        Name = request.Name,
        Created = request.Created,
        IsVisible = request.IsVisible,
        UserId = userId,
        CourseId = courseId,
        Questions = request.Questions.Select(q => q.ToQuestion()).ToList()
    };
}
