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

        public async Task AddAsync(Account account)
        {
            await _dynamoDbContext.SaveAsync<AccountDbEntity>(account.ToDatabase())
                .ConfigureAwait(false);
        }

        public async Task<List<Account>> GetAllAsync(Guid targetId, AccountType accountType)
        {
            /*ScanCondition scanCondition_targetid = new ScanCondition("TargetId", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, targetId);
            ScanCondition scanCondition_accountType = new ScanCondition("AccountType", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, accountType);
            List<ScanCondition> scanConditions = new List<ScanCondition>();
            scanConditions.Add(scanCondition_targetid);
            scanConditions.Add(scanCondition_accountType);*/

            Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig scanOperationConfig
                = new Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig();
            Amazon.DynamoDBv2.DocumentModel.ScanFilter scanFilter
                = new Amazon.DynamoDBv2.DocumentModel.ScanFilter();
            scanFilter.AddCondition("TargetId", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, targetId);
            scanFilter.AddCondition("AccountType", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, Enum.GetName(typeof(AccountType), accountType));
            scanOperationConfig.Filter = scanFilter;
            scanOperationConfig.Limit = 1;
            List<AccountDbEntity> data = await _dynamoDbContext.FromScanAsync<AccountDbEntity>(scanOperationConfig)
                .GetRemainingAsync()
                .ConfigureAwait(false);

            /*List<AccountDbEntity> data = await _dynamoDbContext
                .ScanAsync<AccountDbEntity>(scanConditions)
                .GetRemainingAsync()
                .ConfigureAwait(false);*/

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

        public async Task UpdateAsync(Account account)
        {
            await _dynamoDbContext.SaveAsync(account.ToDatabase()).ConfigureAwait(false);
        }
    }
}
