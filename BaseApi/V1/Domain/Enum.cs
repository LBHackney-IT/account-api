using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetType
    {
        Housing, Garage
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountStatus
    {
        Active, Suspended, Ended
    }
}
