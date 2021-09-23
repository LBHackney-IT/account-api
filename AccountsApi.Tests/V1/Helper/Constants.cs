using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using AccountsApi.V1.Boundary.Response;

namespace AccountsApi.Tests.V1.Helper
{
    public static class Constants
    {
        #region Account

        public static Guid ID { get; } = Guid.NewGuid();
        public const TargetType TARGETTYPE = TargetType.Tenure;
        public static Guid TARGETID { get; } = Guid.NewGuid();
        public const AccountType ACCOUNTTYPE = AccountType.Master;
        public const RentGroupType RENTGROUPTYPE = RentGroupType.Garages;
        public const string AGREEMENTTYPE = "agreementtype";
        public const decimal ACCOUNTBALANCE = 0;
        public const string CREATEDBY = "Admin";
        public const string LASTUPDATEDBY = "Staff001";
        public static DateTime CREATEDDATE { get; } = DateTime.Parse("2020-05-05T15:10:37.471Z");
        public static DateTime LASTUPDATEDDATE { get; } = DateTime.Parse("2020-05-05T15:10:37.471Z");
        public static DateTime STARTDATE { get; } = DateTime.Parse("2020-05-05T15:10:37.471Z");
        public static DateTime ENDDATE { get; } = DateTime.Parse("2020-05-05T15:10:37.471Z");
        public const AccountStatus ACCOUNTSTATUS = AccountStatus.Active;
        public static Guid PARENTACCOUNT { get; } = Guid.NewGuid();
        public const string PAYMENTREFERENCE = "123234345";
        public static Tenure TENURE { get; } = new Tenure()
        {
            FullAddress = "Hamilton Avenue 12",
            TenancyType = "Introductory",
            TenancyId = "123456",
            PrimaryTenants = new[]
            {
                new PrimaryTenant{FullName = "John"},
                new PrimaryTenant{FullName = "Fredrick"}
            }
        };
        #endregion

        #region ConsolidatedCharge
        public const string TYPE = "Electricity";
        public const string FREQUENCY = "Weekly";
        public const decimal AMOUNT = 125.12M;

        public static IEnumerable<ConsolidatedCharge> CONSOLIDATEDCHARGES { get; } = new ConsolidatedCharge[]
        {
            new ConsolidatedCharge
            {
                Frequency = "Monthly", Amount = 1232000.32M, Type = "Elevator"
            },
            new ConsolidatedCharge
            {
                Frequency = "Monthly", Amount = 363.44M, Type = "Cleaning"
            }
        };

        #endregion

        public static Account ConstructAccountFromConstants()
        {
            return new Account
            {
                Id = ID,
                TargetId = TARGETID,
                TargetType = TARGETTYPE,
                AccountBalance = ACCOUNTBALANCE,
                ConsolidatedCharges = new[]
                {
                    new ConsolidatedCharge()
                    {
                        Amount = AMOUNT,
                        Frequency = FREQUENCY,
                        Type = TYPE
                    }
                },
                LastUpdatedDate = LASTUPDATEDDATE,
                Tenure = new Tenure
                {
                    FullAddress = TENURE.FullAddress,
                    TenancyType = TENURE.TenancyType,
                    TenancyId = TENURE.TenancyId
                },
                CreatedBy = CREATEDBY,
                LastUpdatedBy = LASTUPDATEDBY,
                CreatedDate = CREATEDDATE,
                AccountStatus = ACCOUNTSTATUS,
                AccountType = ACCOUNTTYPE,
                AgreementType = AGREEMENTTYPE,
                EndDate = ENDDATE,
                RentGroupType = RENTGROUPTYPE,
                StartDate = STARTDATE
            };
        }

        public static AccountRequest ConstructorAccountRequestFromConstants()
        {
            return new AccountRequest
            {
                ParentAccount = PARENTACCOUNT,
                PaymentReference = PAYMENTREFERENCE,
                AccountStatus = ACCOUNTSTATUS,
                AgreementType = AGREEMENTTYPE,
                AccountType = ACCOUNTTYPE,
                CreatedBy = CREATEDBY,
                EndDate = ENDDATE,
                RentGroupType = RENTGROUPTYPE,
                StartDate = STARTDATE,
                TargetId = TARGETID,
                TargetType = TARGETTYPE
            };
        }

        public static AccountResponse ConstructorAccountModelFromConstants()
        {
            return new AccountResponse
            {
                Id = ID,
                ParentAccount = PARENTACCOUNT,
                PaymentReference = PAYMENTREFERENCE,
                AccountStatus = ACCOUNTSTATUS,
                AgreementType = AGREEMENTTYPE,
                AccountType = ACCOUNTTYPE,
                CreatedBy = CREATEDBY,
                CreatedDate = CREATEDDATE,
                EndDate = ENDDATE,
                LastUpdatedBy = LASTUPDATEDBY,
                LastUpdatedDate = LASTUPDATEDDATE,
                RentGroupType = RENTGROUPTYPE,
                StartDate = STARTDATE,
                TargetId = TARGETID,
                TargetType = TARGETTYPE,
                AccountBalance = ACCOUNTBALANCE,
                Tenure = TENURE,
                ConsolidatedCharges = CONSOLIDATEDCHARGES
            };
        }
    }
}
