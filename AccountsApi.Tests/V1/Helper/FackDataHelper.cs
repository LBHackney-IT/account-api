using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.Model;
using AutoFixture;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        static Guid _guid = Guid.NewGuid();
        public static List<object[]> GetTestData { get; } = new List<object[]>
        {
            new object[] {null, null},
            new object[] {null, "Recharge"},
            new object[] {_guid.ToString(), null},
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

        public static IEnumerable<ConsolidatedCharge> ConsolidatedChargesFakeData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new ConsolidatedCharge
                {
                    Frequency = RandomStringHelper.Get(25),
                    Amount = RandomNumberHelper.Get(),
                    Type = RandomStringHelper.Get(25)
                };
            }
        }

        public static IEnumerable<PrimaryTenant> PrimaryTenantsFakeData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new PrimaryTenant
                {
                    FullName = RandomStringHelper.Get(50)
                };
            }
        }
        private static readonly Fixture _fixture = new Fixture();
        private static IEnumerable<object[]> ReturnData()
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return new object[]
                {
                    _fixture.Create<AccountDbEntity>()
                };
            }
        }

    }

    public static class MockAccountRequestToDomainObject
    {
        public static IEnumerable<object[]> GetTestData => ReturnData();

        private static readonly Fixture _fixture = new Fixture();
        private static IEnumerable<object[]> ReturnData()
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return new object[]
                {
                    _fixture.Create<AccountRequest>()
                };
            }
        }

    }

    public static class MockAccountModelToADomainObject
    {
        public static IEnumerable<object[]> GetTestData => ReturnData();

        private static readonly Fixture _fixture = new Fixture();
        private static IEnumerable<object[]> ReturnData()
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return new object[]
                {
                    _fixture.Create<AccountModel>()
                };
            }
        }

    }

    public static class MockAccount
    {
        public static IEnumerable<object[]> GetTestData => ReturnData();

        private static readonly Fixture _fixture = new Fixture();
        private static IEnumerable<object[]> ReturnData()
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return new object[]
                {
                    _fixture.Create<Account>()
                };
            }
        }

    }

    public static class FakeDataHelper
    {
        private static readonly Fixture _fixture = new Fixture();

        public static QueryResponse MockQueryResponse<T>()
        {
            QueryResponse response = new QueryResponse();
            if (typeof(T) == typeof(AccountModel))
            {
                response.Items.Add(
                new Dictionary<string, AttributeValue>()
                    {
                        { "id", new AttributeValue { S = _fixture.Create<Guid>().ToString() } },
                        { "parent_account", new AttributeValue { S = _fixture.Create<Guid>().ToString() } },
                        { "payment_reference", new AttributeValue { S = _fixture.Create<string>() } },
                        { "target_type", new AttributeValue { S = _fixture.Create<TargetType>().ToString() } },
                        { "target_id", new AttributeValue { S = _fixture.Create<Guid>().ToString() } },
                        { "account_type", new AttributeValue { S = _fixture.Create<AccountType>().ToString() } },
                        { "rent_group_type", new AttributeValue { S = _fixture.Create<RentGroupType>().ToString() } },
                        { "agreement_type", new AttributeValue { S = _fixture.Create<string>() } },
                        { "account_balance", new AttributeValue { N = _fixture.Create<decimal>().ToString("F") } },
                        { "created_by", new AttributeValue { S = _fixture.Create<string>() } },
                        { "last_updated_by", new AttributeValue { S = _fixture.Create<string>() } },
                        { "created_date", new AttributeValue { S = _fixture.Create<DateTime>().ToString("F") } },
                        { "last_updated_date", new AttributeValue { S = _fixture.Create<DateTime>().ToString("F") } },
                        { "start_date", new AttributeValue { S = _fixture.Create<DateTime>().ToString("F") } },
                        { "end_date", new AttributeValue { S = _fixture.Create<DateTime>().ToString("F") } },
                        { "account_status", new AttributeValue { S = _fixture.Create<AccountStatus>().ToString() } },
                        {
                            "consolidated_charges", new AttributeValue
                            {
                                L = /*new List<AttributeValue>*/ Enumerable.Range(0, new Random(10).Next(1, 100))
                                    .Select(p =>
                                        new AttributeValue
                                        {
                                            M =
                                            {
                                                {
                                                    "amount",
                                                    new AttributeValue
                                                    {
                                                        N = _fixture.Create<decimal>().ToString("F")
                                                    }
                                                },
                                                {
                                                    "frequency",
                                                    new AttributeValue { S = _fixture.Create<string>() }
                                                },
                                                { "type", new AttributeValue { S = _fixture.Create<string>() } }
                                            }
                                        }
                                    ).ToList()
                            }
                        },
                        {
                            "tenure",
                            new AttributeValue
                            {
                                M = new Dictionary<string, AttributeValue>
                                {
                                    { "fullAddress", new AttributeValue { S = _fixture.Create<string>() } },
                                    { "tenancyId", new AttributeValue { S = _fixture.Create<string>() } },
                                    { "tenancyType", new AttributeValue { S = _fixture.Create<string>() } }
                                }
                            }
                        }
                    });

            }

            return response;
        }
    }
}
/*
{
                    {"amount",new AttributeValue { N=_fixture.Create<decimal>().ToString("F") }},
                    {"frequency",new AttributeValue { S=_fixture.Create<string>()}},
                    {"type",new AttributeValue { S=_fixture.Create<string>()}}
                }
 */
