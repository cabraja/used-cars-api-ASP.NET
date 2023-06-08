using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Domain.Entities
{
    public class Role:Entity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RoleUseCase> RoleUseCases { get; set; }
    }
}
