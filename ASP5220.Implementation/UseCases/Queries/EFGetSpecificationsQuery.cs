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
    public class EFGetSpecificationsQuery : EFUseCase, IGetSpecificationsQuery
    {
        public EFGetSpecificationsQuery(ASPContext context):base(context)
        {

        }
        public int Id => 4;

        public string Name => "Get all specifications with arrays containing their possible values.";

        public IEnumerable<SpecificationDTO> Execute(BaseSearchDTO search)
        {
            var query = Context.Specifications.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            var response = query.Select(x => new SpecificationDTO 
            { 
                Id = x.Id,
                SpecificationName=x.Name,
                Values = x.SpecificationValues.Select(sv => new SpecificationValueDTO { Id=sv.Id, Value=sv.Value })
            }).ToList();

            return response;
        }
    }
}
