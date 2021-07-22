using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using System.Collections.Generic;
using System.Linq;
using AccountsApi.V1.Boundary.Request;

namespace AccountsApi.V1.Factories
{
    public static class AccountResponseFactory
    {
        public static AccountResponseObject ToResponse(this Account domain)
        {
            return new AccountResponseObject()
            {
                AccountBalance = domain.AccountBalance,
                AccountStatus = domain.AccountStatus,
                EndDate = domain.EndDate,
                LastUpdated = domain.LastUpdated,
                StartDate = domain.StartDate,
                Id = domain.Id,
                TargetId = domain.TargetId,
                TargetType = domain.TargetType,
                AccountType = domain.AccountType,
                AgreementType = domain.AgreementType,
                Tenure = domain.Tenure,
                ConsolidatedCharges = domain.ConsolidatedCharges,
                RentGroupType = domain.RentGroupType
            };
        }

        public static List<AccountResponseObject> ToResponse(this IEnumerable<Account> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
