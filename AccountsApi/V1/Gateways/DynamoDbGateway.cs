using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Gateways.Interfaces;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;

namespace AccountsApi.V1.Gateways
{
    public class DynamoDbGateway : IAccountApiGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IAmazonDynamoDB _amazonDynamoDb;
        private readonly IConfiguration _configuration;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext, IAmazonDynamoDB amazonDynamoDb, IConfiguration configuration)
        {
            _dynamoDbContext = dynamoDbContext;
            _amazonDynamoDb = amazonDynamoDb;
            _configuration = configuration;
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var result = await _dynamoDbContext.LoadAsync<AccountDbEntity>(id).ConfigureAwait(false);

            return result?.ToDomain();
        }

        public async Task AddAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException($"{nameof(account).ToString()} shouldn't be null!");

            await _dynamoDbContext.SaveAsync(account.ToDatabase()).ConfigureAwait(false);
        }

        public async Task<List<Account>> GetAllArrearsAsync(AccountType accountType, string sortBy, Direction direction)
        {
            QueryRequest request = new QueryRequest
            {
                TableName = "Accounts",
                IndexName = "account_type_dx",
                KeyConditionExpression = "account_type = :V_account_type",
                FilterExpression = "account_balance < :V_value",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":V_account_type",new AttributeValue{S = accountType.ToString()}},
                    {":V_value",new AttributeValue{N = "0"}}
                },
                ScanIndexForward = true
            };

            var response = await _amazonDynamoDb.QueryAsync(request).ConfigureAwait(false);
            List<Account> data = response.ToAccounts();

            return data.Sort(sortBy, direction).ToList();
        }

        public async Task<List<Account>> GetAllAsync(Guid targetId, AccountType accountType)
        {
            QueryRequest request = new QueryRequest
            {
                TableName = "Accounts",
                IndexName = "target_id_dx",
                KeyConditionExpression = "target_id = :V_target_id",
                FilterExpression = "account_type = :V_account_type",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":V_target_id",new AttributeValue{S = targetId.ToString()}},
                    {":V_account_type",new AttributeValue{S = accountType.ToString()}}
                },
                ScanIndexForward = true
            };

            var response = await _amazonDynamoDb.QueryAsync(request).ConfigureAwait(false);

            return response.ToAccounts();
        }

        public async Task UpdateAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException($"{nameof(account).ToString()} shouldn't be null");
            await _dynamoDbContext.SaveAsync(account.ToDatabase()).ConfigureAwait(false);
        }
        public async Task<bool> AddBatchAsync(List<Account> accounts)
        {
            var accountsBatch = _dynamoDbContext.CreateBatchWrite<AccountDbEntity>();

            var items = accounts.ToDatabaseList();
            var maxBatchCount = _configuration.GetValue<int>("BatchProcessing:PerBatchCount");
            if (items.Count > maxBatchCount)
            {
                var loopCount = (items.Count / maxBatchCount) + 1;
                for (var start = 0; start < loopCount; start++)
                {
                    var itemsToWrite = items.Skip(start * maxBatchCount).Take(maxBatchCount);
                    accountsBatch.AddPutItems(itemsToWrite);
                    await accountsBatch.ExecuteAsync().ConfigureAwait(false);
                }
            }
            else
            {
                accountsBatch.AddPutItems(items);
                await accountsBatch.ExecuteAsync().ConfigureAwait(false);
            }
            return true;
        }
    }
}
