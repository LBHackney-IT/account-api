using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;

namespace AccountsApi.Tests.V1.Helper
{
    public static class MockParametersForFormatException
    {
        public static List<object[]> GetTestData { get; } = new List<object[]>
        {
            new object[] {RandomStringHelper.Get(255), AccountType.Sundry },
            new object[] {"77018c05-324a-4f45-b3c500acba1e824d", AccountType.Recharge },
        };
    }
    public static class MockParametersForArgumentNullException
    {
        public static List<object[]> GetTestData { get; } = new List<object[]>
        {
            new object[] { null, null },
            new object[] { null, "Recharge" },
            new object[] { "3fa85f64-5717-4562-b3fc-2c963f66a7af", null},
        };
    }

    public static class MockParametersForNotFound
    {
        public static List<object[]> GetTestData { get; } = new List<object[]>
        {
            new object[] { Guid.NewGuid(), AccountType.Master },
            new object[] { Guid.NewGuid(), AccountType.Recharge },
            new object[] { Guid.NewGuid(), AccountType.Sundry }
        };
    }
}
