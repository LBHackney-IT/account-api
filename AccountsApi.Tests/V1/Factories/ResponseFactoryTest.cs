using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AccountsApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        private readonly Fixture _fixture;
        public ResponseFactoryTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToResponseCanMapAccountToAccountModel()
        {
            Account domain = _fixture.Create<Account>();
            var response = domain.ToResponse();
            response.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public void ToResponseNullAccountThrowArgumentNullException()
        {
            Func<AccountModel> func = () => ((Account) null).ToResponse();
            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CanMapAListOfDomainObjectsToAListOfAccountModelObjects()
        {
            var listDomains = Enumerable.Range(0, 2).Select(x => _fixture.Create<Account>()).ToList();

            var listResponses = listDomains.ToResponse();

            listResponses.Should().HaveCount(2);

            listResponses[0].Should().BeEquivalentTo(listDomains[0]);

            listResponses[1].Should().BeEquivalentTo(listDomains[1]);
        }

        [Fact]
        public void CanMapAListOfDomainObjectsToAListOfAccountModelObjects_WhenNull()
        {
            Func<IEnumerable<AccountModel>> func = () => ((List<Account>) null).ToResponse();
            func.Should().Throw<ArgumentNullException>();
        }
    }
}
