using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountsApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        [Fact]
        public void CanMapADomainObjectToAAccountModelObject()
        {
            var domain = new Account
            {
                Id = Guid.Parse("82aa6932-e98d-41a1-a4d4-2b44135554f8"),
                TargetType = TargetType.Block,
                TargetId = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                AccountType = AccountType.Master,
                RentGroupType = RentGroupType.Garages,
                AgreementType = "Agreement type 001",
                AccountBalance = 125.23M,
                CreatedBy = "Admin",
                LastUpdatedBy = "Staff-001",
                CreatedDate = new DateTime(2021, 07, 30),
                LastUpdatedDate = new DateTime(2021, 07, 30),
                StartDate = new DateTime(2021, 07, 30),
                EndDate = new DateTime(2021, 07, 30),
                AccountStatus = AccountStatus.Active,
                ConsolidatedCharges = new List<ConsolidatedCharges>
                {
                    new ConsolidatedCharges
                    {
                        Amount = 125, Frequency = "Weekly", Type = "Water"
                    },
                    new ConsolidatedCharges
                    {
                        Amount = 123, Frequency = "Weekly", Type = "Elevator"
                    }
                },
                Tenure = new Tenure
                {
                    TenancyType = "INT",
                    FullAddress = "Hamilton Street 123 Alley 4.12",
                    TenancyId = "123"
                }
            };

            var response = domain.ToResponse();

            response.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public void CanMapADomainObjectToAnAccountModelObject_WhenNull()
        {
            var domain = (Account) null;

            var response = domain.ToResponse();

            response.Should().BeNull();
        }

        [Fact]
        public void CanMapAListOfDomainObjectsToAListOfAccountModelObjects()
        {
            var listDomains = new List<Account>()
            {
                new Account
                {
                    Id = Guid.Parse("82aa6932-e98d-41a1-a4d4-2b44135554f8"),
                    TargetType = TargetType.Block,
                    TargetId = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                    AccountType = AccountType.Master,
                    RentGroupType = RentGroupType.Garages,
                    AgreementType = "Agreement type 001",
                    AccountBalance = 125.23M,
                    CreatedBy = "Admin",
                    LastUpdatedBy = "Staff-001",
                    CreatedDate = new DateTime(2021, 07, 30),
                    LastUpdatedDate = new DateTime(2021, 07, 30),
                    StartDate = new DateTime(2021, 07, 30),
                    EndDate = new DateTime(2021, 07, 30),
                    AccountStatus = AccountStatus.Active,
                    ConsolidatedCharges = new List<ConsolidatedCharges>
                    {
                        new ConsolidatedCharges
                        {
                            Amount = 125, Frequency = "Weekly", Type = "Water"
                        },
                        new ConsolidatedCharges
                        {
                            Amount = 123, Frequency = "Weekly", Type = "Elevator"
                        }
                    },
                    Tenure = new Tenure
                    {
                        TenancyType = "INT",
                        FullAddress = "Hamilton Street 123 Alley 4.12",
                        TenancyId = "123"
                    }
                },
                new Account
                {
                    Id = Guid.Parse("82aa6932-e98d-41a1-a4d4-2b44135554f1"),
                    TargetType = TargetType.Block,
                    TargetId = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                    AccountType = AccountType.Master,
                    RentGroupType = RentGroupType.Garages,
                    AgreementType = "Agreement type 001",
                    AccountBalance = 125.23M,
                    CreatedBy = "Admin",
                    LastUpdatedBy = "Staff-001",
                    CreatedDate = new DateTime(2021, 07, 30),
                    LastUpdatedDate = new DateTime(2021, 07, 30),
                    StartDate = new DateTime(2021, 07, 30),
                    EndDate = new DateTime(2021, 07, 30),
                    AccountStatus = AccountStatus.Active,
                    ConsolidatedCharges = new List<ConsolidatedCharges>
                    {
                        new ConsolidatedCharges
                        {
                            Amount = 125, Frequency = "Weekly", Type = "Water"
                        },
                        new ConsolidatedCharges
                        {
                            Amount = 123, Frequency = "Weekly", Type = "Elevator"
                        }
                    },
                    Tenure = new Tenure
                    {
                        TenancyType = "INT",
                        FullAddress = "Hamilton Street 123 Alley 4.12",
                        TenancyId = "123"
                    }
                }
            };

            var listResponses = listDomains.ToResponse();

            listResponses.Should().HaveCount(2);

            listResponses[0].Should().BeEquivalentTo(listDomains[0]);

            listResponses[1].Should().BeEquivalentTo(listDomains[1]);
        }

        [Fact]
        public void CanMapAListOfDomainObjectsToAListOfAccountModelObjects_WhenNull()
        {
            var listDomains = (List<Account>) null;

            var listResponses = listDomains.ToResponse();

            listResponses.Should().NotBeNull();

            listResponses.Should().HaveCount(0);
        }
    }
}
