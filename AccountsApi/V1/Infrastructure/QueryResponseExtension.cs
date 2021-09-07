using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Domain;
using Amazon.DynamoDBv2.Model;

namespace AccountsApi.V1.Infrastructure
{
    public static class QueryResponseExtension
    {
        public static List<Account> ToAccounts(this QueryResponse response)
        {
            List<Account> accounts = new List<Account>();
            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                List<ConsolidatedCharge> consolidatedChargesList = null;

                if (item.Keys.Any(p => p == "consolidated_charges"))
                {
                    var charges = item["consolidated_charges"].L;

                    var chargesItems = charges.Select(p => p.M);
                    consolidatedChargesList = new List<ConsolidatedCharge>();
                    foreach (Dictionary<string, AttributeValue> inneritem in chargesItems)
                    {
                        consolidatedChargesList.Add(new ConsolidatedCharge
                        {
                            Amount = decimal.Parse(inneritem["amount"].N),
                            Frequency = inneritem["frequency"].S,
                            Type = inneritem["type"].S
                        });
                    }
                }

                Tenure tenure = null;
                if (item.Keys.Any(p => p == "tenure"))
                {
                    var _tenure = item["tenure"].M;
                    tenure = new Tenure();
                    tenure.FullAddress = _tenure["fullAddress"].S;
                    tenure.TenancyId = _tenure["tenancyId"].S;
                    tenure.TenancyType = _tenure["tenancyType"].S;
                }

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
                    AccountStatus = Enum.Parse<AccountStatus>(item["account_status"].S),
                    PaymentReference = item["payment_reference"].S,
                    ParentAccount = Guid.Parse(item["parent_account"].S)
                });
            }

            return accounts;
        }
    }
}
