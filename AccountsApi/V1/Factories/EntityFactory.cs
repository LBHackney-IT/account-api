using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
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
                CreatedBy = databaseEntity.CreatedBy,
                CreatedDate = databaseEntity.CreatedDate,
                LastUpdatedBy = databaseEntity.LastUpdatedBy,
                LastUpdatedDate = databaseEntity.LastUpdated,
                StartDate = databaseEntity.StartDate,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                AccountType = databaseEntity.AccountType,
                AgreementType = databaseEntity.AgreementType,
                RentGroupType = databaseEntity.RentGroupType,
                ConsolidatedCharges = databaseEntity.ConsolidatedCharges,
                Tenure = databaseEntity.Tenure
            };
        }

        public static Account ToDomain(this AccountRequest databaseEntity)
        {
            return new Account
            {
                AccountStatus = databaseEntity.AccountStatus,
                EndDate = databaseEntity.EndDate,
                CreatedBy = databaseEntity.CreatedBy,
                CreatedDate = databaseEntity.CreatedDate,
                LastUpdatedBy = databaseEntity.LastUpdatedBy,
                LastUpdatedDate = databaseEntity.LastUpdatedDate,
                StartDate = databaseEntity.StartDate,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                AccountType = databaseEntity.AccountType,
                AgreementType = databaseEntity.AgreementType,
                RentGroupType = databaseEntity.RentGroupType
            };
        }

        public static Account ToDomain(this AccountModel databaseEntity)
        {
            Account account = new Account
            {
                Id = databaseEntity.Id,
                AccountBalance = databaseEntity.AccountBalance,
                AccountStatus = databaseEntity.AccountStatus,
                EndDate = databaseEntity.EndDate,
                CreatedBy = databaseEntity.CreatedBy,
                CreatedDate = databaseEntity.CreatedDate,
                LastUpdatedBy = databaseEntity.LastUpdatedBy,
                LastUpdatedDate = databaseEntity.LastUpdatedDate,
                StartDate = databaseEntity.StartDate,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                AccountType = databaseEntity.AccountType,
                AgreementType = databaseEntity.AgreementType,
                RentGroupType = databaseEntity.RentGroupType,
                Tenure = databaseEntity.Tenure,
                ConsolidatedCharges = databaseEntity.ConsolidatedCharges
            };
            return account;
        }

        public static AccountDbEntity ToDatabase(this Account entity)
        {
            return new AccountDbEntity
            {
                Id = entity.Id,
                AccountBalance = entity.AccountBalance,
                TargetType = entity.TargetType,
                TargetId = entity.TargetId,
                StartDate = entity.StartDate,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                LastUpdatedBy = entity.LastUpdatedBy,
                LastUpdated = entity.LastUpdatedDate,
                EndDate = entity.EndDate,
                AccountStatus = entity.AccountStatus,
                AccountType = entity.AccountType,
                AgreementType = entity.AgreementType,
                ConsolidatedCharges = entity.ConsolidatedCharges,
                RentGroupType = entity.RentGroupType,
                Tenure = entity.Tenure
            };
        }
    }
}
