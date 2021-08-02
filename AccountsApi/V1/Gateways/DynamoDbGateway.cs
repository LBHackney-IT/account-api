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
        private readonly DynamoDbContextWrapper _wrapper;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext, DynamoDbContextWrapper wrapper)
        {
            _dynamoDbContext = dynamoDbContext;
            _wrapper = wrapper;
        }

        public async Task AddAsync(Account account)
        {
            await _dynamoDbContext.SaveAsync(account.ToDatabase()).ConfigureAwait(false);
        }

        public async Task<List<Account>> GetAllArrearsAsync(AccountType accountType, string sortBy, Direction direction)
        {
            List<ScanCondition> scanConditions = new List<ScanCondition>();

            ScanCondition scanConditionAccountType = new ScanCondition("AccountType", ScanOperator.Equal, accountType);
            ScanCondition scanConditionAccountBalance = new ScanCondition("AccountBalance", ScanOperator.LessThan, 0.0M);

            scanConditions.Add(scanConditionAccountType);
            scanConditions.Add(scanConditionAccountBalance);

            var data = await _wrapper
                .ScanAsync(_dynamoDbContext, scanConditions)
                .ConfigureAwait(false);

            return data.Sort(sortBy, direction).Select(p => p.ToDomain()).ToList();
        }

        public async Task<List<Account>> GetAllAsync(Guid targetId, AccountType accountType)
        {
            List<ScanCondition> scanConditions = new List<ScanCondition>();

            ScanCondition scanConditionTargetId = new ScanCondition("TargetId", ScanOperator.Equal, targetId);
            ScanCondition scanConditionAccountType = new ScanCondition("AccountType", ScanOperator.Equal, accountType);

            scanConditions.Add(scanConditionTargetId);
            scanConditions.Add(scanConditionAccountType);

            var data = await _wrapper
                .ScanAsync(_dynamoDbContext, scanConditions)
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
