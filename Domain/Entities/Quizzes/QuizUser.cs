using Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Quizzes;
public class QuizUser
{
    public QuizUser(string connectionId, CourseEnum role, List<QuizAnswer> answers, int points)
    {
        ConnectionId = connectionId;
        Role = role;
        Answers = answers;
        Points = points;
    }

    public string ConnectionId { get; set; }
    public CourseEnum Role { get; set; }
    public List<QuizAnswer> Answers { get; set; }
    public int Points { get; set; }
}
