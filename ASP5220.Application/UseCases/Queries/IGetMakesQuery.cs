using ASP5220.Application.UseCases.DTO.Searches;
using ASP5220.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP5220.Application.UseCases.DTO;

namespace ASP5220.Application.UseCases.Queries
{
    public interface IGetMakesQuery:IQuery<BasePaginationSearch,PaginationResponse<MakeDTO>>
    {
    }
}
