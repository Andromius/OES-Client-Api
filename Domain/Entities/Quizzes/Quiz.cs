using Domain.Entities.Common;
using Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quizzes;
public class Quiz : Schedulable
{
    public Quiz()
    {
    }

    public Quiz(string name, DateTime created, bool isVisible, int userId, int courseId, DateTime scheduled, DateTime end) 
        : base(name, created, isVisible, userId, courseId, scheduled, end)
    {
    }
}
