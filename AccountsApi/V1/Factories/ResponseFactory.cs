using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountsApi.V1.Factories
{
    public static class AccountResponseFactory
    {
        public static AccountModel ToResponse(this Account domain)
        {
            if (domain != null)
                return new AccountModel()
                {
                    AccountBalance = domain.AccountBalance,
                    AccountStatus = domain.AccountStatus,
                    EndDate = domain.EndDate,
                    LastUpdatedDate = domain.LastUpdatedDate,
                    LastUpdatedBy = domain.LastUpdatedBy,
                    CreatedDate = domain.CreatedDate,
                    CreatedBy = domain.CreatedBy,
                    StartDate = domain.StartDate,
                    Id = domain.Id,
                    TargetId = domain.TargetId,
                    TargetType = domain.TargetType,
                    AccountType = domain.AccountType,
                    AgreementType = domain.AgreementType,
                    Tenure = domain.Tenure,
                    ConsolidatedCharges = domain.ConsolidatedCharges,
                    RentGroupType = domain.RentGroupType,
                    PaymentReference = domain.PaymentReference,
                    ParentAccount = domain.ParentAccount
                };
            throw new ArgumentNullException("The provided model shouldn't be null");
        }

        public static List<AccountModel> ToResponse(this IEnumerable<Account> domainList)
        {
            if (domainList != null)
                return domainList.Select(domain => domain.ToResponse()).ToList();
            throw new ArgumentNullException("The provided model shouldn't be null");
        }
    }
}
