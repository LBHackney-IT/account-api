using System;
using AccountsApi.V1.Domain;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AccountsApi.Tests.V1.Domain
{
    public class AccountSnsTests
    {
        private static readonly Fixture _fixture = new Fixture();

        [Fact(DisplayName = "Account Sns object should have the correct properties")]
        public void AccountSnsObjectShouldHaveTheCorrectProperties()
        {
            var entityType = typeof(AccountSns);
            entityType.GetProperties().Length.Should().Be(10);
            var entity = _fixture.Create<AccountSns>();

            Assert.IsType<Guid>(entity.Id);
            Assert.IsType<string>(entity.EventType);
            Assert.IsType<string>(entity.SourceDomain);
            Assert.IsType<string>(entity.SourceSystem);
            Assert.IsType<string>(entity.Version);
            Assert.IsType<Guid>(entity.CorrelationId);
            Assert.IsType<DateTime>(entity.DateTime);
            Assert.IsType<User>(entity.User);
            Assert.IsType<Guid>(entity.EntityId);
            Assert.IsType<EventData>(entity.EventData);
        }
    }
}
