using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shelter.Core.Abstractions;
using Shelter.Core.Enums;

namespace Shelter.Core.Entities
{
    public class Employee : Person
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "position")]
        public Position Position { get; set; }

        public bool Active { get; set; }

        public ICollection<Treatment> Treatments { get; set; }
    }
}
