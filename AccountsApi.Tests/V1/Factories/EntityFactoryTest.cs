using AutoFixture;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
using Xunit;

namespace AccountsApi.Tests.V1.Factories
{
    // TODO
    public class EntityFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDomain()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Create<AccountDbEntity>();
            var entity = databaseEntity.ToDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.StartDate.Should().BeSameDateAs(entity.StartDate);
        }

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDatabase()`. Also be sure to add test cases for
        // any edge cases that might exist.
        // [Test]
        // public void CanMapADomainEntityToADatabaseObject()
        // {
        //     var entity = _fixture.Create<AccountDbEntity>();
        //     var databaseEntity = entity.ToDatabase();
        //
        //     entity.Id.Should().Be(databaseEntity.Id);
        //     entity.CreatedAt.Should().BeSameDateAs(databaseEntity.CreatedAt);
        // }
    }
}
