using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public class HomeworkScore
{
    public Homework Homework { get; set; }
    public int HomeworkId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public int Points { get; set; }
}
