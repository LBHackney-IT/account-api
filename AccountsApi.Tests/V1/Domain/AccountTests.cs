using System;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
using Xunit;

namespace AccountsApi.Tests.V1.Domain
{
    public class AccountTests
    {
        [Fact]
        public void EntitiesHaveACreatedAt()
        {
            var entity = new AccountDbEntity();
            var date = new DateTime(2019, 02, 21);
            entity.StartDate = date;

            entity.StartDate.Should().BeSameDateAs(date);
        }
    }
}
