using System;
using System.Linq;

namespace AccountsApi.Tests.V1.Helper
{
    public static class RandomDateHelper
    {
        public enum DateDirection{Future,Previous}
        private static readonly Random _random = new Random();
        public static DateTime Get(DateDirection dateDirection,int minDays,int maxDays)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            if (dateDirection==DateDirection.Previous)
                return start.AddDays(_random.Next(range));
            else
                return start.AddDays(_random.Next(range+minDays, range+maxDays));
        }
    }
}
