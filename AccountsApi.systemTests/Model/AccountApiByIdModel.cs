using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsApi.systemTests.Model
{
    public class AccountApiByIdModel
    {
        public DateTime createdAt { get; set; }
        public DateTime lastUpdatedAt { get; set; }
        public object createdBy { get; set; }
        public object consolidatedCharges { get; set; }
        public string endReasonCode { get; set; }
        public Tenure tenure { get; set; }
        public string id { get; set; }
        public double accountBalance { get; set; }
        public string targetId { get; set; }
        public string targetType { get; set; }
        public string paymentReference { get; set; }
        public string parentAccountId { get; set; }
        public string accountType { get; set; }
        public string rentGroupType { get; set; }
        public string agreementType { get; set; }
        public string accountStatus { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double consolidatedBalance { get; set; }
        public string lastUpdatedBy { get; set; }
    }
}
