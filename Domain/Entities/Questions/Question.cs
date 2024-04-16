using Domain.Entities.Common;
using Domain.Entities.Courses;
using Domain.Entities.Questions.Options;
using Domain.Entities.Tests;
using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Questions;

public class Question
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Option> Options { get; set; } = [];
    public int Points { get; set; }
    public string Type { get; set; }
    public int ItemId { get; set; }
    public CourseItem Item { get; set; }
    public List<Answer> Answers { get; set; } = [];
    public List<AnswerSimilarity> AnswerSimilarities { get; set; } = [];
}
