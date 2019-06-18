using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shelter.Core.Abstractions;
using Shelter.Core.Enums;

namespace Shelter.Core.Entities
{
    public class Transaction : BaseEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "transactionType")]
        public TransactionType Type { get; set; }

        public DateTime Date { get; set; }
        public double Cost { get; set; }
    }
}
