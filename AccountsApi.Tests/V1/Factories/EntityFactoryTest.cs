using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
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
        public void ToDomainMapAccountRequestToDomainObject()
        {
            AccountRequest entity = _fixture.Create<AccountRequest>();
            var domain = entity.ToDomain();
            entity.Should().NotBeNull();
            domain.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public void ToDomainMapAccountModelToDomainObject()
        {
            AccountResponse entity = _fixture.Create<AccountResponse>();
            var domain = entity.ToDomain();
            entity.Should().NotBeNull();
            entity.Should().BeEquivalentTo(domain, opt =>
                 opt.Excluding(a => a.CreatedAt)
                     .Excluding(a => a.CreatedBy)
                     .Excluding(a => a.LastUpdatedAt));
        }

        [Fact]
        public void ToDatabaseMapAccountToAccountDbEntity()
        {
            Account domain = _fixture.Create<Account>();
            AccountDbEntity dbEntity = domain.ToDatabase();
            dbEntity.Should().NotBeNull();
            dbEntity.Should().BeEquivalentTo(domain);
        }

    }
}
