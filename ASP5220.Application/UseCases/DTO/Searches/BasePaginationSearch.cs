using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases.DTO.Searches
{
   public class BasePaginationSearch:PaginationSearch
    {
        public string Keyword { get; set; }
    }
}
