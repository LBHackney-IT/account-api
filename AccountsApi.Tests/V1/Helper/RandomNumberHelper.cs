using System;
using System.Linq;

namespace AccountsApi.Tests.V1.Helper
{
    public static class RandomNumberHelper
    {
        private static readonly Random _random = new Random();
        public static decimal Get(int min,int max)
        {
            return _random.Next(min, max);
        }
    }
}
