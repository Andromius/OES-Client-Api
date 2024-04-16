using Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quizzes;
public record QuizRequest(
    string Name,
    DateTime Created,
    bool IsVisible,
    int CreatedById,
    DateTime End,
    List<QuestionRequest> Questions);

