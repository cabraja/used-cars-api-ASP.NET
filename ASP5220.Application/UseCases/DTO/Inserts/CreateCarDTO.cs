using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases.DTO.Inserts
{
    public class CreateCarDTO
    {
        public int Price { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public int EngineCapacity { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        //public int UserId { get; set; }
        public int MakeId { get; set; }
        public IEnumerable<SpecificationValueDTO> SpecificationValues { get; set; }
        public IEnumerable<FileDTO> Files { get; set; }
    }
}

