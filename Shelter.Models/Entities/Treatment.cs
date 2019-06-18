﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shelter.Core.Abstractions;
using Shelter.Core.Enums;

namespace Shelter.Core.Entities
{
    public class Treatment : BaseEntity
    {
        [JsonConverter(typeof(ICollection<StringEnumConverter>))]
        [JsonProperty(PropertyName = "diseases")]
        public ICollection<Disease> Diseases { get; set; }

        public DateTime Date { get; set; }
        public double Cost { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
