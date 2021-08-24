using System;
using AccountsApi.V1.Domain;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

namespace AccountsApi.Tests.V1.Domain
{
    [TestFixture]
    public class AccountSnsTests
    {
        private static Fixture _fixture = new Fixture();

        [TestCase(TestName = "Account Sns object should have the correct properties")]
        public void AccountSnsObjectShouldHaveTheCorrectProperties()
        {
            var entityType = typeof(AccountSns);
            entityType.GetProperties().Length.Should().Be(10);
            var entity = _fixture.Create<AccountSns>();
            Assert.That(entity, Has.Property("Id").InstanceOf(typeof(Guid)));
            Assert.That(entity, Has.Property("EventType").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("SourceDomain").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("SourceSystem").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Version").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("CorrelationId").InstanceOf(typeof(Guid)));
            Assert.That(entity, Has.Property("DateTime").InstanceOf(typeof(DateTime)));
            Assert.That(entity, Has.Property("User").InstanceOf(typeof(User)));
            Assert.That(entity, Has.Property("EntityId").InstanceOf(typeof(Guid)));
            Assert.That(entity, Has.Property("EventData").InstanceOf(typeof(EventData)));
        }
    }
}
