using System;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace AccountsApi.Tests.V1.Domain
{
    [TestFixture]
    public class EntityTests
    {
        // [Test]
        // public void EntitiesHaveAnId()
        // {
        //     var entity = new AccountDbEntity();
        //     entity.Id.Should().NotBe(null);
        // }

        [Test]
        public void EntitiesHaveACreatedAt()
        {
            var entity = new AccountDbEntity();
            var date = new DateTime(2019, 02, 21);
            entity.StartDate = date;

            entity.StartDate.Should().BeSameDateAs(date);
        }
    }
}
