using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Tests;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quizzes;
public static class QuizExtensions
{
    public static QuizResponse ToResponse(this Quiz quiz, CourseEnum courseParticipant) => new(
        nameof(Quiz),
        quiz.Id,
        quiz.Name,
        quiz.Created,
        quiz.UserId,
        quiz.IsVisible,
        quiz.Scheduled,
        quiz.End,
        quiz.Questions.Select(q => q.ToResponse(courseParticipant)).ToList());

    public static Quiz ToQuiz(this QuizRequest request, int courseId, int userId) => new()
    {
        Name = request.Name,
        Created = request.Created,
        IsVisible = request.IsVisible,
        UserId = userId,
        CourseId = courseId,
        End = request.End,
        Questions = request.Questions.Select(q => q.ToQuestion()).ToList()
    };
}
