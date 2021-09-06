using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using AccountsApi.Tests.V1.Helper;
using Xunit;
using AutoFixture;

namespace AccountsApi.Tests.V1.Factories
{
    public class EntityFactoryTest
    {

        private readonly Fixture _fixture;


        public EntityFactoryTest()
        {
            _fixture = new Fixture();
        }


        [Fact]
        public void ToDomainMapDatabaseEntityToDomainObject()
        {
            AccountDbEntity entity = _fixture.Create<AccountDbEntity>();
            var domain = entity.ToDomain();
            domain.Should().NotBeNull();
            entity.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public void ToDomainNullDatabaseEntityThrowArgumentNullException()
        {
            Func<Account> func = () => ((AccountDbEntity) null).ToDomain();
            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ToDomainMapAccountRequestToDomainObject()
        {
            AccountRequest entity = _fixture.Create<AccountRequest>();
            var domain = entity.ToDomain();
            entity.Should().NotBeNull();
            domain.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public void ToDomainNullAccountRequestThrowArgumentNullException()
        {
            Func<Account> func = () => ((AccountRequest) null).ToDomain();
            func.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public void ToDomainMapAccountModelToDomainObject()
        {
            AccountModel entity = _fixture.Create<AccountModel>();
            var domain = entity.ToDomain();
            entity.Should().NotBeNull();
            entity.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public void ToDomainNullAccountModelThrowArgumentNullException()
        {
            Func<Account> func = () => ((AccountModel) null).ToDomain();
            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ToDatabaseMapAccountToAccountDbEntity()
        {
            Account domain = _fixture.Create<Account>();
            AccountDbEntity dbEntity = domain.ToDatabase();
            dbEntity.Should().NotBeNull();
            dbEntity.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public void ToDatabaseNullAccountThrowArgumentNullException()
        {
            Func<AccountDbEntity> func = () => ((Account) null).ToDatabase();
            func.Should().Throw<ArgumentNullException>();
        }
    }
}
