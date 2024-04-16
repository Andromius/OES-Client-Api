using Domain.Entities.Courses;
using Domain.Entities.Questions;
using Domain.Entities.Questions.Options;
using Domain.Entities.Tests;
using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Questions;

public static class QuestionExtensions
{
    public static List<Question> ToQuestionList(this List<QuestionRequest> questionRequests)
    {
        List<Question> questions = new();
        foreach (var questionRequest in questionRequests)
        {
            Question q = questionRequest.ToQuestion();
            questions.Add(q);
        }

        return questions;
    }

    public static Question ToQuestion(this QuestionRequest questionRequest)
    {
        Question q = new()
        {
            Description = questionRequest.Description,
            Name = questionRequest.Name,
            Options = questionRequest.Options.Select(o => o.ToOption(questionRequest.Options.IndexOf(o) + 1)).ToList(),
            Points = questionRequest.Points,
            Type = questionRequest.Type,
        };
        return q;
    }

    public static QuestionResponse ToResponse(this Question question, CourseEnum courseParticipant, bool returnPointsAnyway = false) => new
        (question.Id.Value, question.Name, question.Description, question.Points, question.Type)
    { Options = question.Options.Select(o => o.ToResponse(courseParticipant, returnPointsAnyway)).ToList() };
}
