using Domain.Entities.Questions.Options;
using Domain.Entities.Tests.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Questions;

public record QuestionRequest(string Name, string Description, List<OptionRequest> Options, int Points, string Type);
