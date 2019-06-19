using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shelter.Core.Abstractions;
using Shelter.Core.Enums;

namespace Shelter.Core.Entities
{
    public class Volunteer : Person
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "helpKind")]
        public HelpKind HelpKind { get; set; }

        public ICollection<Sponsorship> Sponsorships { get; set; }
    }
}
