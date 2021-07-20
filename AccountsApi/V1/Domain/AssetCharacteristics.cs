using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class AssetCharacteristics
    {
        public short NumberOfBedrooms { get; set; }
        public short NumberOfLifts { get; set; }
        public short NumberOfLivingRooms { get; set; }
        public string WindowType { get; set; }
        public int YearConstructed { get; set; }
    }
}
