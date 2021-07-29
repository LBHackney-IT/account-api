using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Gateways
{
    public class DynamoDbGateway : IAccountApiGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task AddAsync(Account account)
        {
            await _dynamoDbContext.SaveAsync(account.ToDatabase()).ConfigureAwait(false);
        }

        public async Task<List<Account>> GetAllAsync(Guid targetId, AccountType accountType)
        {
            List<ScanCondition> scanConditions = new List<ScanCondition>();

            ScanCondition scanConditionTargetId = new ScanCondition("TargetId", ScanOperator.Equal, targetId);
            ScanCondition scanConditionAccountType = new ScanCondition("AccountType", ScanOperator.Equal, accountType);

            scanConditions.Add(scanConditionTargetId);
            scanConditions.Add(scanConditionAccountType);

            var data = await _dynamoDbContext
                .ScanAsync<AccountDbEntity>(scanConditions)
                .GetRemainingAsync()
                .ConfigureAwait(false);

            return data.Select(p => p.ToDomain()).ToList();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var result = await _dynamoDbContext.LoadAsync<AccountDbEntity>(id).ConfigureAwait(false);

            return result?.ToDomain();
        }

        public async Task RemoveAsync(Account account)
        {
            await _dynamoDbContext.DeleteAsync(account.ToDatabase()).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Account account)
        {
            await _dynamoDbContext.SaveAsync(account.ToDatabase()).ConfigureAwait(false);
        }
    }
}
