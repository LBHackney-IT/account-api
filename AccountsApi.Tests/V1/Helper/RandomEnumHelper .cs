using System;
using System.Linq;

namespace AccountsApi.Tests.V1.Helper
{
    public class RandomEnumHelper<T> 
    {
        private readonly Random _random = new Random(); 
        public T Get()
        {
            Type type = typeof(T);
            var items = Enum.GetValues(type);
            return (T)Enum.GetValues(type).GetValue(_random.Next(items.Length));
        }
    }
}
