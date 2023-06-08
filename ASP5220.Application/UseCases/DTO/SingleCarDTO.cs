using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases.DTO
{
    public class SingleCarDTO
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        public int EngineCapacity { get; set; }
        public string Make { get; set; }
        public UserDTO Seller { get; set; }
        public ICollection<FileDTO> Files { get; set; }
        public ICollection<SpecificationAndValueDTO> Specifications { get; set; }
        public int FollowersCount { get; set; }
    }
}
