using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Questions;
public class OptionResponse
{
    public OptionResponse(int id, string text, int? points)
    {
        Id = id;
        Text = text;
        Points = points;
    }

    public int Id { get; set; }
    public string Text { get; set; }
    public int? Points { get; set; }
}
