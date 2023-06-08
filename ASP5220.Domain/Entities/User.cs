using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class User:Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<CarUser> FollowedCars { get; set; } = new List<CarUser>();
        public Role Role { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
