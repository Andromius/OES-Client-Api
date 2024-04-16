using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Questions;

namespace Domain.Entities.Questions.Options;
public class Option
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Option() { }
    public Option(int id, string text, int points)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Id = id;
        Text = text;
        Points = points;
    }

    public int Id { get; set; }
    public string Text { get; set; }
    public int Points { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
