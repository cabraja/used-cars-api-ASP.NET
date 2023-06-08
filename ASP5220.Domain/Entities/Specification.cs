using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class Specification:Entity
    {
        public string Name { get; set; }
        public ICollection<SpecificationValue> SpecificationValues { get; set; }
        public  ICollection<SpecificationCar> SpecificationCars { get; set; }
    }
}
