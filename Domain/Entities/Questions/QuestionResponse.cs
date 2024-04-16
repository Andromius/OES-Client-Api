using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Questions.Options;

namespace Domain.Entities.Questions;

public class QuestionResponse
{
    public QuestionResponse(int id, string name, string description, int points, string type)
    {
        Id = id;
        Name = name;
        Description = description;
        Points = points;
        Type = type;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<OptionResponse> Options { get; set; }
    public int Points { get; set; }
    public string Type { get; set; }
}
