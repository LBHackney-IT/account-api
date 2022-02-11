using System.Text.Json.Serialization;

namespace AccountsApi.V1.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetType
    {
        Tenure
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
        Ms,
        Dr
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Master, Recharge, Sundry
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RentGroupType
    {
        Tenant,
        LeaseHolders,
        GenFundRents,
        Garages,
        HaLeases,
        HraRents,
        MajorWorks,
        TempAcc,
        Travelers,
        GarParkHRA,
        HousingGenFund,
        HousingRevenue,
        LHMajorWorks,
        LHServCharges,
        RSLandXBorough,
        TempAccGenFun,
        TempAccomHRA,
        TravelGenFund
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Direction
    {
        Asc,
        Desc
    }
}
