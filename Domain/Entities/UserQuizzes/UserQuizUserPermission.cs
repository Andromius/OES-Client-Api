using Domain.Entities.Common;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserQuizzes;
public class UserQuizUserPermission
{
    public int UserQuizId { get; set;}
    public UserQuiz UserQuiz { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public EUserPermission Permission { get; set; }
}
