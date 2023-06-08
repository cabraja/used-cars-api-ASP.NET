using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class File:Entity
    {
        public string Path { get; set; }
        public Car Car { get; set; }
    }
}
