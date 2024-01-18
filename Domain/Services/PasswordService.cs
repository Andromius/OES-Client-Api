using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services;

public class PasswordService
{
    public static string GetHash(string input)
    {
        return BCrypt.Net.BCrypt.HashPassword(input);
    }

    public static bool CompareHash(string toCompare, string toCompareWith)
    {
        return BCrypt.Net.BCrypt.Verify(toCompare, toCompareWith);
    }
}
