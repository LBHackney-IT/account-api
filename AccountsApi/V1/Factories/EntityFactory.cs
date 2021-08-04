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
            return databaseEntity == null ? null : new Account
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
                ConsolidatedCharges = databaseEntity.ConsolidatedCharges,
                Tenure = databaseEntity.Tenure
            };
        }

        public static Account ToDomain(this AccountRequest request)
        {
            return request == null ? null : new Account
            {
                AccountStatus = request.AccountStatus,
                EndDate = request.EndDate,
                CreatedBy = request.CreatedBy,
                CreatedDate = request.CreatedDate,
                LastUpdatedBy = request.LastUpdatedBy,
                LastUpdatedDate = request.LastUpdatedDate,
                StartDate = request.StartDate,
                TargetId = request.TargetId,
                TargetType = request.TargetType,
                AccountType = request.AccountType,
                AgreementType = request.AgreementType,
                RentGroupType = request.RentGroupType
            };
        }

        public static Account ToDomain(this AccountModel model)
        {
            return model == null ? null : new Account
            {
                Id = model.Id,
                AccountBalance = model.AccountBalance,
                AccountStatus = model.AccountStatus,
                EndDate = model.EndDate,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                LastUpdatedBy = model.LastUpdatedBy,
                LastUpdatedDate = model.LastUpdatedDate,
                StartDate = model.StartDate,
                TargetId = model.TargetId,
                TargetType = model.TargetType,
                AccountType = model.AccountType,
                AgreementType = model.AgreementType,
                RentGroupType = model.RentGroupType,
                ConsolidatedCharges = model.ConsolidatedCharges,
                Tenure = model.Tenure
            };
        }

        public static AccountDbEntity ToDatabase(this Account account)
        {
            return account == null ? null : new AccountDbEntity
            {
                Id = account.Id,
                AccountBalance = account.AccountBalance,
                AccountStatus = account.AccountStatus,
                EndDate = account.EndDate,
                CreatedBy = account.CreatedBy,
                CreatedDate = account.CreatedDate,
                LastUpdatedBy = account.LastUpdatedBy,
                LastUpdatedDate = account.LastUpdatedDate,
                StartDate = account.StartDate,
                TargetId = account.TargetId,
                TargetType = account.TargetType,
                AccountType = account.AccountType,
                AgreementType = account.AgreementType,
                RentGroupType = account.RentGroupType,
                ConsolidatedCharges = account.ConsolidatedCharges,
                Tenure = account.Tenure
            };
        }
    }
}
