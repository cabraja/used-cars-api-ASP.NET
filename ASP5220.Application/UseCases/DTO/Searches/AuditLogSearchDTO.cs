using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases.DTO.Searches
{
    public class AuditLogSearchDTO:PaginationSearch
    {
        public string Username { get; set; }
        public string UseCaseName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
