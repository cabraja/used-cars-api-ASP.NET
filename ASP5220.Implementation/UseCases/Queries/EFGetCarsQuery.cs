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
    public class EFGetCarsQuery : EFUseCase, IGetCarsQuery
    {
        public EFGetCarsQuery(ASPContext context) : base(context)
        {

        }
        public int Id => 6;

        public string Name => "Get paginated cars";

        public PaginationResponse<CarDTO> Execute(BasePaginationSearch search)
        {
            var query = Context.Cars.Include(x => x.User).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Model.Contains(search.Keyword) || x.Variant.Contains(search.Keyword) || x.Make.Name.Contains(search.Keyword) || x.User.Username.Contains(search.Keyword));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;


            var response = new PaginationResponse<CarDTO>();

            response.TotalCount = query.Count();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;
            response.PageCount = (int)Math.Ceiling((float)response.TotalCount / search.PerPage.Value);
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new CarDTO
            { 
                Id=x.Id,
                Price=x.Price,
                Model=x.Model,
                Variant=x.Variant,
                Mileage=x.Mileage,
                Power=x.Power,
                EngineCapacity=x.EngineCapacity,
                Make=x.Make.Name,
                Seller=x.User.Username,
                Files= x.Files.Select(f => new FileDTO { Path=f.Path}).ToList(),
                FollowersCount=x.Followers.Count
            }).ToList();

            return response;
        }
    }
}
