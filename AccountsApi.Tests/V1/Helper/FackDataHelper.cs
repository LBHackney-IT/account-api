using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.Tests.V1.Helper
{
    public static class MockParametersForFormatException
    {
        public static List<object[]> GetTestData { get; } = new List<object[]>
        {
            new object[] {RandomStringHelper.Get(255), AccountType.Sundry},
            new object[] {RandomStringHelper.Get(255), AccountType.Master},
            new object[] {RandomStringHelper.Get(255), AccountType.Recharge}
        };
    }

    public static class MockParametersForArgumentNullException
    {
        public static List<object[]> GetTestData { get; } = new List<object[]>
        {
            new object[] {null, null},
            new object[] {null, "Recharge"},
            new object[] {"3fa85f64-5717-4562-b3fc-2c963f66a7af", null},
        };
    }

    public static class MockParametersForNotFound
    {
        public static List<object[]> GetTestData { get; } = new List<object[]>
        {
            new object[] {Guid.NewGuid(), AccountType.Master},
            new object[] {Guid.NewGuid(), AccountType.Recharge},
            new object[] {Guid.NewGuid(), AccountType.Sundry}
        };
    }

    public static class MockDatabaseEntityToADomainObject
    {
        public static IEnumerable<object[]> GetTestData => ReturnData();

        private static IEnumerable<ConsolidatedCharges> ConsolidatedChargesFakeData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new ConsolidatedCharges
                {
                    Frequency = RandomStringHelper.Get(25),
                    Amount = RandomNumberHelper.Get(),
                    Type = RandomStringHelper.Get(25)
                };
            }
        }

        private static IEnumerable<object[]> ReturnData()
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return new object[]
                {
                    new AccountDbEntity()
                    {
                        Id = Guid.NewGuid(),
                        TargetType = TargetType.Block,
                        TargetId = Guid.NewGuid(),
                        AccountType = AccountType.Master,
                        RentGroupType = RentGroupType.Garages,
                        AgreementType = RandomStringHelper.Get(100),
                        AccountBalance = RandomNumberHelper.Get(5, 18),
                        CreatedBy = RandomStringHelper.Get(20),
                        LastUpdatedBy = RandomStringHelper.Get(20),
                        CreatedDate = RandomDateHelper.Get(RandomDateHelper.DateDirection.Previous, 0, 0),
                        LastUpdatedDate = RandomDateHelper.Get(RandomDateHelper.DateDirection.Previous, 0, 0),
                        StartDate = RandomDateHelper.Get(RandomDateHelper.DateDirection.Future, 0, 365),
                        EndDate = RandomDateHelper.Get(RandomDateHelper.DateDirection.Future, 365, 365 * 2),
                        AccountStatus = new RandomEnumHelper<AccountStatus>().Get(),
                        ConsolidatedCharges = ConsolidatedChargesFakeData(10)
                            /*new[]
                            {
                                new ConsolidatedCharges
                                {
                                    Amount = RandomNumberHelper.Get(),
                                    Frequency = RandomStringHelper.Get(10),
                                    Type = RandomStringHelper.Get(10)
                                }
                            },*/
                        ,Tenure = new Tenure
                        {
                            FullAddress = RandomStringHelper.Get(100),
                            TenancyId = RandomStringHelper.Get(25),
                            TenancyType = RandomStringHelper.Get(10)
                        }
                    }
                };
            }
        }
    }
}
