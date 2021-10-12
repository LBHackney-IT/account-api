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
                ParentAccountId = databaseEntity.ParentAccountId,
                PaymentReference = databaseEntity.PaymentReference,
                AccountBalance = databaseEntity.AccountBalance,
                ConsolidatedBalance = databaseEntity.ConsolidatedBalance,
                AccountStatus = databaseEntity.AccountStatus,
                EndDate = databaseEntity.EndDate,
                CreatedBy = databaseEntity.CreatedBy,
                CreatedAt = databaseEntity.CreatedAt,
                LastUpdatedBy = databaseEntity.LastUpdatedBy,
                LastUpdatedAt = databaseEntity.LastUpdatedAt,
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
            return new Account
            {
                AccountStatus = request.AccountStatus,
                CreatedBy = request.CreatedBy,
                TargetId = request.TargetId,
                TargetType = request.TargetType,
                AccountType = request.AccountType,
                AgreementType = request.AgreementType,
                RentGroupType = request.RentGroupType,
                ParentAccountId = request.ParentAccountId,
                PaymentReference = request.PaymentReference,
                Tenure = request.Tenure
            };
        }

        public static Account ToDomain(this AccountResponse model)
        {
            return new Account
            {
                Id = model.Id,
                AccountBalance = model.AccountBalance,
                ConsolidatedBalance = model.ConsolidatedBalance,
                AccountStatus = model.AccountStatus,
                EndDate = model.EndDate,
                LastUpdatedBy = model.LastUpdatedBy,
                StartDate = model.StartDate,
                TargetId = model.TargetId,
                TargetType = model.TargetType,
                AccountType = model.AccountType,
                AgreementType = model.AgreementType,
                RentGroupType = model.RentGroupType,
                ConsolidatedCharges = model.ConsolidatedCharges,
                Tenure = model.Tenure,
                ParentAccountId = model.ParentAccountId,
                PaymentReference = model.PaymentReference
            };
        }

        public static AccountDbEntity ToDatabase(this Account account)
        {
            return new AccountDbEntity
            {
                Id = account.Id,
                AccountBalance = account.AccountBalance,
                ConsolidatedBalance = account.ConsolidatedBalance,
                AccountStatus = account.AccountStatus,
                EndDate = account.EndDate,
                CreatedBy = account.CreatedBy,
                CreatedAt = account.CreatedAt,
                LastUpdatedBy = account.LastUpdatedBy,
                LastUpdatedAt = account.LastUpdatedAt,
                StartDate = account.StartDate,
                TargetId = account.TargetId,
                TargetType = account.TargetType,
                AccountType = account.AccountType,
                AgreementType = account.AgreementType,
                RentGroupType = account.RentGroupType,
                ConsolidatedCharges = account.ConsolidatedCharges,
                Tenure = account.Tenure,
                PaymentReference = account.PaymentReference,
                ParentAccountId = account.ParentAccountId
            };
        }
    }
}
