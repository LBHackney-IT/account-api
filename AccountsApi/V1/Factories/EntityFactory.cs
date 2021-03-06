using System.Collections.Generic;
using System.Linq;
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
            Account account = new Account
            {
                Id = databaseEntity.Id,
                ParentAccountId = databaseEntity.ParentAccountId,
                PaymentReference = databaseEntity.PaymentReference,
                EndReasonCode = databaseEntity.EndReasonCode,
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
            return account;
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
                EndReasonCode = request.EndReasonCode,
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
                PaymentReference = model.PaymentReference,
                EndReasonCode = model.EndReasonCode
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
                EndReasonCode = account.EndReasonCode,
                ParentAccountId = account.ParentAccountId
            };
        }
        public static List<AccountDbEntity> ToDatabaseList(this List<Account> accounts)
        {
            return accounts.Select(item => item.ToDatabase()).ToList();
        }

        public static List<Account> ToDomainList(this List<AccountRequest> accounts)
        {
            return accounts.Select(item => item.ToDomain()).ToList();
        }
    }
}
