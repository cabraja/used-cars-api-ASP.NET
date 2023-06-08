using ASP5220.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases.Queries
{
    public interface IGetSingleUserQuery:IQuery<int,UserDTO>
    {
    }
}
