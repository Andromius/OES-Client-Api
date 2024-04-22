using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;
public class UserQuizPositionPointsResponse
{
    public int UserId { get; set; }
    public int Points { get; set; }
    public int Position { get; set; }
}
