using RestSharp;

namespace AccountsApi.systemTests.Model
{

    public class AccountResponseList
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public object CreatedBy { get; set; }
        public object ConsolidatedCharges { get; set; }
        public string EndReasonCode { get; set; }
        public Tenure Tenure { get; set; }
        public string Id { get; set; }
        public double AccountBalance { get; set; }
        public string TargetId { get; set; }
        public string TargetType { get; set; }
        public string PaymentReference { get; set; }
        public string ParentAccountId { get; set; }
        public string AccountType { get; set; }
        public string RentGroupType { get; set; }
        public string AgreementType { get; set; }
        public string AccountStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double ConsolidatedBalance { get; set; }
        public string LastUpdatedBy { get; set; }
    }

    public class PrimaryTenant
    {
        public string Id { get; set; }
        public string FullName { get; set; }
    }

    public class AccountApiModel
    {
        public List<AccountResponseList> AccountResponseList { get; set; }

        public static implicit operator RestResponse<object>(AccountApiModel v)
        {
            throw new NotImplementedException();
        }
    }

    public class Tenure
    {
        public string TenureId { get; set; }
        public TenureType TenureType { get; set; }
        public string FullAddress { get; set; }
        public List<PrimaryTenant> PrimaryTenants { get; set; }
    }

    public class TenureType
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
