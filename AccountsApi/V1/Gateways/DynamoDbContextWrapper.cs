using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountsApi.V1.Gateways
{
    public class DynamoDbContextWrapper
    {
        public virtual Task<List<AccountDbEntity>> ScanAsync(IDynamoDBContext context, IEnumerable<ScanCondition> conditions, DynamoDBOperationConfig operationConfig = null)
        {
            return context.ScanAsync<AccountDbEntity>(conditions, operationConfig).GetRemainingAsync();
        }
    }
}
