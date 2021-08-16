using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace AccountsApi.V1.Gateways
{
    public class DynamoDbGateway : IAccountApiGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly DynamoDbContextWrapper _wrapper;
        private readonly IAmazonDynamoDB _amazonDynamoDb;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext, DynamoDbContextWrapper wrapper, IAmazonDynamoDB amazonDynamoDb)
        {
            _dynamoDbContext = dynamoDbContext;
            _wrapper = wrapper;
            this._amazonDynamoDb = amazonDynamoDb;
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
            QueryRequest request = new QueryRequest
            {
                TableName = "accounts",
                IndexName = "target_id",
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

            List<Account> accounts = new List<Account>();
            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                List<ConsolidatedCharges> consolidatedChargesList = null;

                try
                {
                    var charges = item["consolidated_charges"].L;
                    var chargesItems = charges.Select(p => p.M);
                    consolidatedChargesList = new List<ConsolidatedCharges>();
                    foreach (Dictionary<string, AttributeValue> inneritem in chargesItems)
                    {
                        consolidatedChargesList.Add(new ConsolidatedCharges
                        {
                            Amount = decimal.Parse(inneritem["amount"].N),
                            Frequency = inneritem["frequency"].S,
                            Type = inneritem["type"].S
                        });
                    }
                }
                catch{}

                Tenure tenure = null;
                try
                {
                    var _tenure = item["tenure"].M;
                    tenure = new Tenure();
                    tenure.FullAddress = _tenure["fullAddress"].S;
                    tenure.TenancyId = _tenure["tenancyId"].S;
                    tenure.TenancyType = _tenure["tenancyType"].S;
                }
                catch{}

                accounts.Add(new Account
                {
                    Id = Guid.Parse(item["id"].S),
                    AccountBalance = decimal.Parse(item["account_balance"].N),
                    ConsolidatedCharges = consolidatedChargesList,
                    Tenure = tenure,
                    TargetType = Enum.Parse<TargetType>(item["target_type"].S),
                    TargetId = Guid.Parse(item["target_id"].S),
                    AccountType = Enum.Parse<AccountType>(item["account_type"].S),
                    RentGroupType = Enum.Parse<RentGroupType>(item["rent_group_type"].S),
                    AgreementType = item["agreement_type"].S,
                    CreatedBy = item["created_by"].S,
                    LastUpdatedBy = item["last_updated_by"].S,
                    CreatedDate = DateTime.Parse(item["created_date"].S),
                    LastUpdatedDate = DateTime.Parse(item["last_updated_date"].S),
                    StartDate = DateTime.Parse(item["start_date"].S),
                    EndDate = DateTime.Parse(item["end_date"].S),
                    AccountStatus = Enum.Parse<AccountStatus>(item["account_status"].S)
                });
            }

            return accounts;
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
