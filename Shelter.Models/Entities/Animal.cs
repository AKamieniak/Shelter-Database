using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shelter.Core.Abstractions;
using Shelter.Core.Enums;

namespace Shelter.Core.Entities
{
    public class Animal : BaseEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "race")]
        public Race Race { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "feature")]
        public Feature Feature { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public bool Adopted { get; set; }
        public bool Status { get; set; }
        public DateTime DateOfArrival { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Treatment> Treatments { get; set; }
    }
}
