using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using System;

namespace AccountsApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Account ToDomain(this AccountDbEntity databaseEntity)
        {
            if (databaseEntity != null)
                return new Account
                {
                    Id = databaseEntity.Id,
                    ParentAccount = databaseEntity.ParentAccount,
                    PaymentReference = databaseEntity.PaymentReference,
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
            throw new ArgumentNullException("The provided model shouldn't be null");
        }

        public static Account ToDomain(this AccountRequest request)
        {
            if (request != null)
                return new Account
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
                    RentGroupType = request.RentGroupType,
                    ParentAccount = request.ParentAccount,
                    PaymentReference = request.PaymentReference
                };
            throw new ArgumentNullException("The provided model shouldn't be null");
        }

        public static Account ToDomain(this AccountModel model)
        {
            if (model != null)
                return new Account
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
                    Tenure = model.Tenure,
                    ParentAccount = model.ParentAccount,
                    PaymentReference = model.PaymentReference
                };
            throw new ArgumentNullException("The provided model shouldn't be null");
        }

        public static AccountDbEntity ToDatabase(this Account account)
        {
            if (account != null)
                return new AccountDbEntity
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
                    Tenure = account.Tenure,
                    PaymentReference = account.PaymentReference,
                    ParentAccount = account.ParentAccount
                };
            throw new ArgumentNullException("The provided model shouldn't be null");
        }
    }
}
