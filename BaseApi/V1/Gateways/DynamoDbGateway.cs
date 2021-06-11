using Amazon.DynamoDBv2.DataModel;
using AccountApi.V1.Domain;
using AccountApi.V1.Factories;
using AccountApi.V1.Infrastructure;
using System.Collections.Generic;

namespace AccountApi.V1.Gateways
{
    public class DynamoDbGateway : IExampleGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public List<Account> GetAll()
        {
            return new List<Account>();
        }

        public Account GetEntityById(int id)
        {
            var result = _dynamoDbContext.LoadAsync<AccountDbEntity>(id).GetAwaiter().GetResult();
            return result?.ToDomain();
        }
    }
}
