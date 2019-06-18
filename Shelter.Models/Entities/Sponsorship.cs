using System;
using Shelter.Core.Abstractions;

namespace Shelter.Core.Entities
{
    public class Sponsorship : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Cost { get; set; }

        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}
