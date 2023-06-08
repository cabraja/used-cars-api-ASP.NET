using ASP5220.Application.UseCases.DTO;
using ASP5220.Application.UseCases.DTO.Searches;
using ASP5220.Application.UseCases.Queries;
using ASP5220.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.UseCases.Queries
{
    public class EFGetMakesQuery :EFUseCase, IGetMakesQuery
    {
        public string Name => "Search makes";

        public int Id => 3;

        public EFGetMakesQuery(ASPContext context):base(context)
        {
            
        }

        public PaginationResponse<MakeDTO> Execute(BasePaginationSearch search)
        {
            var query = Context.Makes.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            if(search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;


            var response = new PaginationResponse<MakeDTO>();

            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new MakeDTO { Name = x.Name, CarCount = x.Cars.Count });
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;
            response.PageCount = (int)Math.Ceiling((float)response.TotalCount / search.PerPage.Value);

            return response;
        }
    }
}
