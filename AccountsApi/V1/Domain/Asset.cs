using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string AssetId { get; set; }
        public AssetType AssetType { get; set; }
        public AssetAddress AssetAddress { get; set; }
        public Tenure Tenancy { get; set; }
        public InnerAccount InnerAccount { get; set; }
        public AssetCharacteristics AssetCharacteristics { get; set; }
        public short StairCasing { get; set; }
    }
}
