using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AccountsApi.V1.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetType
    {
        Estate,Block,Core,Flat
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountStatus
    {
        Active, Suspended, Ended
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Title
    {
        Mr,
        Mrs,
        Miss,
        Ms,
        Dr
    }

    public enum AccountType
    {
        Master,Recharge, Sundry
    }

    public enum RentGroupType
    {
        LeaseHolders,GenFundRents,Garages,HALeases,HRARents, MajorWorks,TempAcc,Travellers
    }
}
