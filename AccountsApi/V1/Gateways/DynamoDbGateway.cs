using Amazon.DynamoDBv2.DataModel;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

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
            List<ScanCondition> scanConditions = new List<ScanCondition>();
            List<AccountDbEntity> data = new List<AccountDbEntity>();

            /*Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig scanOperationConfig
                = new Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig();
            Amazon.DynamoDBv2.DocumentModel.ScanFilter scanFilter
                = new Amazon.DynamoDBv2.DocumentModel.ScanFilter();
            scanFilter.AddCondition("TargetId", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, targetId);
            scanFilter.AddCondition("AccountType", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, Enum.GetName(typeof(AccountType), accountType));
            scanOperationConfig.Filter = scanFilter;
            scanOperationConfig.Limit = 2;
            List<AccountDbEntity> data = await _dynamoDbContext.FromScanAsync<AccountDbEntity>(scanOperationConfig)
                .GetRemainingAsync()
                .ConfigureAwait(false);*/

            /*ScanCondition scanConditionTargetId = new ScanCondition("TargetId", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, targetId);
            ScanCondition scanConditionAccountType = new ScanCondition("AccountType", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, accountType);
            
            scanConditions.Add(scanConditionTargetId);
            scanConditions.Add(scanConditionAccountType);

            data = await _dynamoDbContext
                .ScanAsync<AccountDbEntity>(scanConditions)
                .GetRemainingAsync()
                .ConfigureAwait(false);*/

            /*AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            QueryRequest request = new QueryRequest()
            {
                TableName = "accounts",
                IndexName =  "target_id"
            };*/

            /*AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            ScanRequest request = new ScanRequest("accounts")
            {
                Limit = 10
            };
            var response =client.ScanAsync(request);

            var data = response.Result;*/
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            config.ServiceURL = "http://localhost:8000";
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(config);
            Expression expression = new Expression();
            expression.ExpressionAttributeNames.Add("#A","TargetId");
            expression.ExpressionAttributeNames.Add("#B","AccountType");

            expression.ExpressionAttributeValues.Add("#A", targetId);
            expression.ExpressionAttributeValues.Add("#B", Enum.GetName(typeof(AccountType), accountType));

            var request = new ScanRequest
            {
                TableName = "accounts",
                FilterExpression = expression.ExpressionStatement,
                Limit = 2
            };

            var response = client.ScanAsync(request);
            var result = response.Result;

            data = response.ToDatabase();

            /*foreach (Dictionary<string, AttributeValue> item in response.Result.Items)
            {
                // Process the result.
                var x = item;
                //data.Add(item.);
            }*/

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
