using Amazon.DynamoDBv2.DataModel;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace AccountsApi.V1.Gateways
{
    public class DynamoDbGateway : IAccountApiGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public void Add(Account account)
        {
            _dynamoDbContext.SaveAsync<AccountDbEntity>(account.ToDatabase());
        }

        public async Task AddAsync(Account account)
        {
            await _dynamoDbContext.SaveAsync<AccountDbEntity>(account.ToDatabase())
                .ConfigureAwait(false);
        }

        public void AddRange(List<Account> accounts)
        {
            accounts.ForEach(account =>
            {
                Add(account);
            });
        }

        public async Task AddRangeAsync(List<Account> accounts)
        {
            foreach (Account account in accounts)
            {
                await AddAsync(account).ConfigureAwait(false);
            }
        }

        public async Task<List<Account>> GetAllAsync(Guid targetId, string accountType)
        {
            ScanCondition scanCondition_targetid = new ScanCondition("TargetId", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, targetId);
            List<ScanCondition> scanConditions = new List<ScanCondition>();
            scanConditions.Add(scanCondition_targetid);

            List<AccountDbEntity> data = await _dynamoDbContext
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
            await _dynamoDbContext.DeleteAsync<AccountDbEntity>(account.ToDatabase())
                .ConfigureAwait(false);
        }

        public async Task RemoveRangeAsync(List<Account> accounts)
        {
            foreach (Account account in accounts)
            {
                await RemoveAsync(account).ConfigureAwait(false);
            }
        }

        public void Update(Account account)
        {
            _dynamoDbContext.SaveAsync<AccountDbEntity>(account.ToDatabase());
        }

        public async Task UpdateAsync(Account account)
        {
            await _dynamoDbContext.SaveAsync(account.ToDatabase()).ConfigureAwait(false);
        }
    }
}
