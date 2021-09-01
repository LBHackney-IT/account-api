using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AccountsApi.Tests.V1.Gateways
{
    [TestFixture]
    public class DynamoDbGatewayTests
    {
        private Mock<IDynamoDBContext> _dynamoDb;

        [SetUp]
        public void Setup()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
        }
    }
}
