using AccountApi.V1.Domain;
using AccountsApi.V1.Domain;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsApi.V1.Infrastructure
{
    [Table("accounts")]
    [DynamoDBTable("accounts", LowerCamelCaseProperties = true)]
    public class AccountDbEntity
    {
        [DynamoDBHashKey]
        [DynamoDBProperty(AttributeName = "id")]
        public Guid Id { get; set; }
        [DynamoDBProperty(AttributeName = "target_type", Converter = typeof(DynamoDbEnumConverter<TargetType>))]
        public TargetType TargetType { get; set; }
        [DynamoDBProperty(AttributeName = "target_id")]
        public Guid TargetId { get; set; }
        [DynamoDBProperty(AttributeName = "account_type",Converter = typeof(DynamoDbEnumConverter<AccountType>))]
        public AccountType AccountType { get; set; }
        [DynamoDBProperty(AttributeName ="rent_group_type", Converter =typeof(DynamoDbEnumConverter<RentGroupType>))]
        public RentGroupType RentGroupType { get; set; }
        [DynamoDBProperty(AttributeName ="agreement_type")]
        public string AgreementType { get; set; }
        [DynamoDBProperty(AttributeName = "account_balance")]
        public decimal AccountBalance { get; set; }
        [DynamoDBProperty(AttributeName = "created_by")]
        public string CreatedBy { get; set; }
        [DynamoDBProperty(AttributeName = "last_updated_by")]
        public string LastUpdatedBy { get; set; }
        [DynamoDBProperty(AttributeName = "created_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime CreatedDate { get; set; }
        [DynamoDBProperty(AttributeName = "last_updated", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime LastUpdated { get; set; }
        [DynamoDBProperty(AttributeName = "start_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime StartDate { get; set; }
        [DynamoDBProperty(AttributeName = "end_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime EndDate { get; set; }
        [DynamoDBProperty(AttributeName = "account_status",Converter = typeof(DynamoDbEnumConverter<AccountStatus>))]
        public AccountStatus AccountStatus { get; set; }

        [DynamoDBProperty(AttributeName = "consolidated_charges",Converter = typeof(DynamoDbObjectListConverter<ConsolidatedCharge>))]
        public IEnumerable<ConsolidatedCharge> ConsolidatedCharges { get; set; }

        [DynamoDBProperty(AttributeName = "tenure",Converter = typeof(DynamoDbObjectConverter<Tenure>))]
        public Tenure Tenure { get; set; }
    }
}
