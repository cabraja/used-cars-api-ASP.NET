using ASP5220.Application.UseCases.DTO;
using ASP5220.Application.UseCases.DTO.Searches;
using ASP5220.Application.UseCases.Queries;
using ASP5220.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.UseCases.Queries
{
    public class EFGetAuditLogsQuery : EFUseCase, IGetAuditLogsQuery
    {
        public EFGetAuditLogsQuery(ASPContext context):base(context)
        {

        }
        public int Id => 10;

        public string Name => "Serach paginated audit logs";

        public PaginationResponse<AuditLogDTO> Execute(AuditLogSearchDTO search)
        {
            var query = Context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.Username.Contains(search.Username));
            }


            if (!string.IsNullOrEmpty(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.Contains(search.UseCaseName));
            }


            //CHECK DATES
            if(search.FromDate.HasValue)
            {
               query =  query.Where(x => DateTime.Compare(x.ExecutionTime, search.FromDate.Value) > 0);
            }

            if (search.ToDate.HasValue)
            {
                query = query.Where(x => DateTime.Compare(x.ExecutionTime, search.ToDate.Value) < 0);
            }

            //PAGINATION CHECK
            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PaginationResponse<AuditLogDTO>();


            var res = query.ToList();

            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new AuditLogDTO 
            { 
                UseCaseName=x.UseCaseName,
                Username=x.Username,
                ExecutionTime=x.ExecutionTime,
                Data = x.Data,
                IsAuthorized = x.IsAuthorized

            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;
            response.PageCount = (int)Math.Ceiling((float)response.TotalCount / search.PerPage.Value);

            return response;
        }
    }
}
