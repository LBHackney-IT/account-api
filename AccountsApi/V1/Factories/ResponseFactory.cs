using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using System.Collections.Generic;
using System.Linq;
using AccountsApi.V1.Boundary.Request;

namespace AccountsApi.V1.Factories
{
    public static class AccountResponseFactory
    {
        public static AccountResponse ToResponse(this Account domain)
        {
            return new AccountResponse()
            {
                AccountBalance = domain.AccountBalance,
                TotalBalance = domain.TotalBalance,
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
        }

        public static AccountResponse ToResponse(this AccountUpdate domain)
        {
            return new AccountResponse()
            {
                AccountBalance = domain.AccountBalance,
                TotalBalance = domain.TotalBalance,
                AccountStatus = domain.AccountStatus,
                EndDate = domain.EndDate,
                LastUpdatedBy = domain.LastUpdatedBy,
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
        }

        public static AccountUpdate ToUpdateModel(this AccountResponse response)
        {
            return new AccountUpdate()
            {
                AccountBalance = response.AccountBalance,
                TotalBalance = response.TotalBalance,
                AccountStatus = response.AccountStatus,
                EndDate = response.EndDate,
                LastUpdatedBy = response.LastUpdatedBy,
                StartDate = response.StartDate,
                Id = response.Id,
                TargetId = response.TargetId,
                TargetType = response.TargetType,
                AccountType = response.AccountType,
                AgreementType = response.AgreementType,
                Tenure = response.Tenure,
                ConsolidatedCharges = response.ConsolidatedCharges,
                RentGroupType = response.RentGroupType,
                PaymentReference = response.PaymentReference,
                ParentAccount = response.ParentAccount
            };
        }

        public static List<AccountResponse> ToResponse(this IEnumerable<Account> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
