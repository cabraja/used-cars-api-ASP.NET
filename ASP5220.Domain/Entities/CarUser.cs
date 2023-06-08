using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class CarUser
    {
        public int CarId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
    }
}
