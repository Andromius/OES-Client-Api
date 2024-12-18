using Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserQuizzes;
public record UserQuizRequest(
    string Name,
    DateTime Created,
    bool IsVisible,
    int CreatedById,
    bool ShouldShuffleQuestions,
    List<QuestionRequest> Questions);
