using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.Model;

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
        public static Account ToDomain(this AccountRequestObject databaseEntity)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
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
                RentGroupType = databaseEntity.RentGroupType
            };
        }
        public static Account ToDomain(this AccountResponseObject databaseEntity)
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
                Tenure = databaseEntity.Tenure,
                ConsolidatedCharges = databaseEntity.ConsolidatedCharges
            };
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

        public static List<AccountDbEntity> ToDatabase(this Task<ScanResponse> response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            List<AccountDbEntity>  entities = new List<AccountDbEntity>();

            foreach (Dictionary<string,AttributeValue> item in response.Result.Items)
            {
                entities.Add(item.ToDatabase());    
            }

            return entities;
        }

        private static AccountDbEntity ToDatabase(this Dictionary<string, AttributeValue> entity)
        {
            return new AccountDbEntity
            {
                Id = Guid.Parse(entity["Id"].S),
                AccountBalance = decimal.Parse(entity["AccountBalance"].N),
                TargetType = Enum.Parse<TargetType>(entity["TargetType"].S),
                TargetId = Guid.Parse(entity["TargetId"].S),
                StartDate = DateTime.Parse(entity["StartDate"].S),
                CreatedBy = entity["CreatedBy"].S,
                CreatedDate = DateTime.Parse(entity["CreatedDate"].S),
                LastUpdatedBy = entity["LastUpdatedBy"].S,
                LastUpdated = DateTime.Parse(entity["LastUpdatedDate"].S),
                EndDate = DateTime.Parse(entity["EndDate"].S),
                AccountStatus = Enum.Parse<AccountStatus>(entity["AccountStatus"].N),
                AccountType = Enum.Parse<AccountType>(entity["AccountType"].S),
                AgreementType = entity["AgreementType"].S,
                //ConsolidatedCharges = (ConsolidatedCharges)entity["ConsolidatedCharges"].S
                RentGroupType = Enum.Parse<RentGroupType>(entity["RentGroupType"].S),
                //Tenure = entity.Tenure
            };
        }
    }
}
