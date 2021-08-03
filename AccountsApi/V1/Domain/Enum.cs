using System.Text.Json.Serialization;

namespace AccountsApi.V1.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetType
    {
        Estate, Block, Core, Flat
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
        LeaseHolders, GenFundRents, Garages, HaLeases, HraRents, MajorWorks, TempAcc, Travellers
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Direction
    {
        Asc,
        Desc
    }
}
