using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class SpecificationValue:Entity
    {
        public string Value { get; set; }
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
        public ICollection<SpecificationCar> SpecificationCars { get; set; }
    }
}
