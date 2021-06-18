using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Account ToDomain(this AccountDbEntity databaseEntity)
        {
            return new Account
            {
                Id = databaseEntity.Id,
                AccountBalance = databaseEntity.AccountBalance,
                AccountStatus = databaseEntity.AccountStatus,
                EndDate = databaseEntity.EndDate,
                LastUpdated = databaseEntity.LastUpdated,
                PaymentReference = databaseEntity.PaymentReference,
                StartDate = databaseEntity.StartDate,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                TotalCharged = databaseEntity.TotalCharged,
                TotalPaid = databaseEntity.TotalPaid
            };
        }

        public static AccountDbEntity ToDatabase(this Account entity)
        {
            return new AccountDbEntity
            {
                Id = entity.Id,
                AccountBalance = entity.AccountBalance,
                TotalPaid = entity.TotalPaid,
                TotalCharged = entity.TotalCharged,
                TargetType = entity.TargetType,
                TargetId = entity.TargetId,
                StartDate = entity.StartDate,
                PaymentReference = entity.PaymentReference,
                LastUpdated = entity.LastUpdated,
                EndDate = entity.EndDate,
                AccountStatus = entity.AccountStatus
            };
        }
    }
}
