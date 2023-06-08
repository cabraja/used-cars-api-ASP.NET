using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases.DTO
{
    public class SpecificationDTO
    {
        public int Id { get; set; }
        public string SpecificationName { get; set; }
        public IEnumerable<SpecificationValueDTO> Values { get; set; }
    }
}
