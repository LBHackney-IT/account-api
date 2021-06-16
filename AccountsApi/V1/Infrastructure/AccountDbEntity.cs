using AccountsApi.V1.Domain;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsApi.V1.Infrastructure
{
    [Table("accounts")]
    [DynamoDBTable("accounts", LowerCamelCaseProperties = true)]
    public class AccountDbEntity
    {
        [DynamoDBHashKey]
        [DynamoDBProperty(AttributeName ="id")]
        public Guid Id { get; set; }
        [DynamoDBProperty(AttributeName = "target_id")]
        public Guid TargetId { get; set; }
        [DynamoDBProperty(AttributeName ="target_type",Converter = typeof(DynamoDbEnumConverter<TargetType>))]
        public TargetType TargetType { get; set; }
        [DynamoDBProperty(AttributeName ="account_balance")]
        public decimal AccountBalance { get; set; }
        [DynamoDBProperty(AttributeName = "payment_reference")]
        public string PaymentReference { get; set; }
        [DynamoDBProperty(AttributeName = "last_updated",Converter =typeof(DynamoDbDateTimeConverter))]
        public DateTime LastUpdated { get; set; }
        [DynamoDBProperty(AttributeName = "start_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime StartDate { get; set; }
        [DynamoDBProperty(AttributeName = "end_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime EndDate { get; set; }
        [DynamoDBProperty(AttributeName = "account_status", Converter = typeof(DynamoDbEnumConverter<AccountStatus>))]
        public AccountStatus AccountStatus { get; set; }
        [DynamoDBProperty(AttributeName ="total_charged")]
        public decimal TotalCharged { get; set; }
        [DynamoDBProperty(AttributeName = "total_paid")]
        public decimal TotalPaid { get; set; }
    }
}
