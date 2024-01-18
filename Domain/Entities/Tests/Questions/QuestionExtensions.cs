using Domain.Entities.Courses;
using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Questions;

public static class QuestionExtensions
{
    public static List<Question> ToQuestionList(this List<QuestionRequest> questionRequests)
    {
        List<Question> questions = new();
        foreach (var questionRequest in questionRequests)
        {
            Question q = questionRequest.ToQuestion();
            q.Id = questionRequests.IndexOf(questionRequest) + 1;
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
        q.Options.ForEach(o => o.Id = q.Options.IndexOf(o) + 1);
        return q;
    }

    public static QuestionResponse ToResponse(this Question question, CourseEnum who) => new
        (question.Id, question.Name, question.Description, question.Points, question.Type)
    { Options = question.Options.Select(o => o.ToResponse(who)).ToList() };
}
