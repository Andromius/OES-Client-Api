using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services;
public class CourseCodeGenerationService
{
    private readonly Random _random = new();
    public string GenerateUniqueCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder codeBuilder = new();

        for (int i = 0; i < 5; i++)
        {
            int index = _random.Next(chars.Length);
            codeBuilder.Append(chars[index]);
        }

        return codeBuilder.ToString();
    }
}
