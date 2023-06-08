using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases
{
    public interface IUseCase
    {
        public int Id { get; }
        string Name { get; }
    }
}
