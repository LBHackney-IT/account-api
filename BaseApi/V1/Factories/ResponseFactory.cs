using AccountApi.V1.Boundary.Response;
using AccountApi.V1.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AccountApi.V1.Factories
{
    public static class AccountResponseFactory
    {
        public static AccountResponseObject ToResponse(this Account domain)
        {
            return new AccountResponseObject() {
                AccountBalance = domain.AccountBalance,
                AccountStatus = domain.AccountStatus,
                EndDate = domain.EndDate,
                LastUpdated = domain.LastUpdated,
                PaymentReference= domain.PaymentReference,
                StartDate = domain.StartDate,
                Id = domain.Id,
                TargetId = domain.TargetId,
                TargetType = domain.TargetType,
                TotalCharged = domain.TotalCharged,
                TotalPaid = domain.TotalPaid
            };
        }

        public static List<AccountResponseObject> ToResponse(this IEnumerable<Account> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
