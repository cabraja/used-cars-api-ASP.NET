using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class SpecificationCar
    {
        public int CarId { get; set; }
        public int SpecificationId { get; set; }
        public int SpecificationValueId { get; set; }
        public Car Car { get; set; }
        public Specification Specification { get; set; }
        public SpecificationValue SpecificationValue { get; set; }
    }
}
