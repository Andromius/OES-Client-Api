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
    private const int RANDOM_PASSWORD_LENGTH = 12;
    public static string GetHash(string input)
    {
        return BCrypt.Net.BCrypt.HashPassword(input);
    }

    public static bool CompareHash(string toCompare, string toCompareWith)
    {
        return BCrypt.Net.BCrypt.Verify(toCompare, toCompareWith);
    }

    public static string GenerateRandomPassword()
    {
        const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string specialChars = "!@#$%^&*()_-+={}[]|\\:;'<,>.?/";

        var charSets = new string[] { lowercaseChars, uppercaseChars, digits, specialChars };
        var password = new StringBuilder();

        foreach (var charSet in charSets)
        {
            password.Append(charSet[GetRandomIndex(charSet.Length)]);
        }

        int remainingLength = RANDOM_PASSWORD_LENGTH - charSets.Length;
        password.Append(GetRandomString(remainingLength, string.Join("", charSets)));

        return password.ToString();
    }
    private static int GetRandomIndex(int maxValue)
    {
        if (maxValue <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxValue));
        }

        byte[] randomBytes = RandomNumberGenerator.GetBytes(sizeof(int));
        int randomInt = BitConverter.ToInt32(randomBytes, 0);
        return Math.Abs(randomInt % maxValue);
    }
    private static string GetRandomString(int length, string chars)
    {
        StringBuilder builder = new();
        for (int i = 0; i < length; i++)
        {
            builder.Append(chars[GetRandomIndex(chars.Length)]);
        }
        return builder.ToString();
    }
}
