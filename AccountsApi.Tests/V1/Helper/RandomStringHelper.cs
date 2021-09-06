using System;
using System.Linq;

namespace AccountsApi.Tests.V1.Helper
{
    public static class RandomStringHelper
    {
        private static readonly Random _random = new Random();
        public static string Get(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
