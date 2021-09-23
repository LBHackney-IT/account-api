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
                ParentAccount = databaseEntity.ParentAccount,
                PaymentReference = databaseEntity.PaymentReference,
                AccountBalance = databaseEntity.AccountBalance,
                TotalBalance = databaseEntity.TotalBalance,
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
            return new Account
            {
                AccountStatus = request.AccountStatus,
                EndDate = request.EndDate,
                CreatedBy = request.CreatedBy,
                StartDate = request.StartDate,
                TargetId = request.TargetId,
                TargetType = request.TargetType,
                AccountType = request.AccountType,
                AgreementType = request.AgreementType,
                RentGroupType = request.RentGroupType,
                ParentAccount = request.ParentAccount,
                PaymentReference = request.PaymentReference
            };
        }

        public static Account ToDomain(this AccountResponse model)
        {
            return new Account
            {
                Id = model.Id,
                AccountBalance = model.AccountBalance,
                TotalBalance = model.TotalBalance,
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
                ParentAccount = model.ParentAccount,
                PaymentReference = model.PaymentReference
            };
        }

        public static AccountDbEntity ToDatabase(this Account account)
        {
            return new AccountDbEntity
            {
                Id = account.Id,
                AccountBalance = account.AccountBalance,
                TotalBalance = account.TotalBalance,
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
                Tenure = account.Tenure,
                PaymentReference = account.PaymentReference,
                ParentAccount = account.ParentAccount
            };
        }
    }
}
