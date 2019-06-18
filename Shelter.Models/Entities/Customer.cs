using System.Collections.Generic;
using Shelter.Core.Abstractions;

namespace Shelter.Core.Entities
{
    public class Customer : Person
    {
        public ICollection<Animal> Animals { get; set; }
    }
}
