using ASP5220.Application;
using ASP5220.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP5220.API.Core
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public IEnumerable<int> AllowedUseCases { get; set; }

        public string Role { get; set; }
    }

    public class AnonymusActor : IApplicationActor
    {
        public int Id => 0;

        public string Email => "anonimus@mail.com";

        public string Username => "Anonymus";

        public IEnumerable<int> AllowedUseCases { get; set; } = new List<int> { 1,3,6};

        public string Role => "Anonymus";
    }
}


