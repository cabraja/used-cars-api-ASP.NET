using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class Car:Entity
    {
        public int Price { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        public int EngineCapacity { get; set; }
        public User User { get; set; }
        public Make Make { get; set; }
        public ICollection<File> Files { get; set; }
        public ICollection<SpecificationCar> Specifications { get; set; }
        public ICollection<CarUser> Followers { get; set; } = new List<CarUser>();

    }
}
