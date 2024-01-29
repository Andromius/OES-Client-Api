using Domain.Entities.Tests.Answers;
using Domain.Entities.Tests.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Checkers;
public static class TestChecker
{
    public static void Check(Test test, TestSubmission submission)
    {
        if (test.Id != submission.TestId) throw new Exception("Ids don't match");

        List<Question> questions = test.Questions.Where(q => q.Type != "open").ToList();
        int totalPoints = 0;
        for (int i = 0; i < questions.Count; i++)
        {
            Question question = questions[i];
            List<Option> options = question.Options;
            switch(question.Type)
            {
                case QuestionType.PickOne: CheckSingle(options, submission.Answers.Single(a => a.QuestionId == question.Id), ref totalPoints); break;
                case QuestionType.PickMany: CheckMultiple(options, submission.Answers.FindAll(a => a.QuestionId == question.Id), ref totalPoints); break;
            };
        }
        submission.TotalPoints = totalPoints;
        submission.Status = test.Questions.Exists(q => q.Type == "open") ?
            Common.ESubmissionStatus.Checked : Common.ESubmissionStatus.Graded;
    }

    private static void CheckSingle(List<Option> options, Answer answer, ref int totalPoints) 
        => totalPoints += options.Single(o => o.Id == answer.Id).Points;

    private static void CheckMultiple(List<Option> options, List<Answer> answers, ref int totalPoints)
    {
        foreach (var option in options)
        {
            totalPoints += answers.Exists(a => a.Id == option.Id) ? option.Points : 0;
        }
    }
}
