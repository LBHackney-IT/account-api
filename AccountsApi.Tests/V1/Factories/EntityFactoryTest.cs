using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
using System;
using System.Collections.Generic;
using AccountsApi.Tests.V1.Helper;
using Xunit;

namespace AccountsApi.Tests.V1.Factories
{
    public class EntityFactoryTest
    {
        [Theory]
        [MemberData(nameof(MockDatabaseEntityToADomainObject.GetTestData),MemberType = typeof(MockDatabaseEntityToADomainObject))]
        public void CanMapADatabaseEntityToADomainObject(AccountDbEntity entity) {
            
            var domain = entity.ToDomain();

            entity.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public void CanMapADatabaseEntityToADomainObject_WhenNull()
        {
            var entity = (AccountDbEntity) null;

            var domain = entity.ToDomain();

            domain.Should().BeNull();
        }

        [Fact]
        public void CanMapADomainObjectToADatabaseEntity()
        {
            var domain = new Account
            {
                Id = Guid.Parse("82aa6932-e98d-41a1-a4d4-2b44135554f8"),
                ParentAccount = Guid.Parse("82aa6932-e98d-41a1-a4d4-2b4413555o8f"),
                PaymentReference = "234345456",
                TargetType = TargetType.Tenure,
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
                    TenancyId = "123",
                    PrimaryTenants = new[]
                    {
                        new PrimaryTenant{FullName = "John"},
                        new PrimaryTenant{FullName = "Fredrick"}
                    }
                }
            };

            var entity = domain.ToDatabase();

            entity.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public void CanMapADomainObjectToADatabaseEntity_WhenNull()
        {
            var domain = (Account) null;

            var entity = domain.ToDatabase();

            entity.Should().BeNull();
        }

        [Fact]
        public void CanMapARequestObjectToDomainObject()
        {
            var request = new AccountRequest
            {
                TargetType = TargetType.Tenure,
                TargetId = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                ParentAccount = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fn89j"),
                AccountType = AccountType.Master,
                RentGroupType = RentGroupType.Garages,
                AgreementType = "Agreement type 001",
                CreatedBy = "Admin",
                LastUpdatedBy = "Staff-001",
                CreatedDate = new DateTime(2021, 07, 30),
                LastUpdatedDate = new DateTime(2021, 07, 30),
                StartDate = new DateTime(2021, 07, 30),
                EndDate = new DateTime(2021, 07, 30),
                AccountStatus = AccountStatus.Active
            };

            var entity = request.ToDomain();

            entity.Should().BeEquivalentTo(request);
        }

        [Fact]
        public void CanMapARequestObjectToDomainObject_WhenNull()
        {
            var request = (AccountRequest) null;

            var domain = request.ToDomain();

            domain.Should().BeNull();
        }

        [Fact]
        public void CanMapAnAccountModelObjectToDomainObject()
        {
            var accountModel = new Account
            {
                Id = Guid.Parse("82aa6932-e98d-41a1-a4d4-2b44135554f8"),
                ParentAccount = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fh5r3"),
                PaymentReference = "123234345",
                TargetType = TargetType.Tenure,
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
                    TenancyId = "123",
                    PrimaryTenants = new[]
                    {
                        new PrimaryTenant{FullName = "John"},
                        new PrimaryTenant{FullName = "Fredrick"}
                    }
                }
            };

            var domain = accountModel.ToDatabase();

            domain.Should().BeEquivalentTo(accountModel);
        }

        [Fact]
        public void CanMapAnAccountModelObjectToDomainObject_WhenNull()
        {
            var accountModel = (AccountModel) null;

            var domain= accountModel.ToDomain();

            domain.Should().BeNull();
        }
    }
}
