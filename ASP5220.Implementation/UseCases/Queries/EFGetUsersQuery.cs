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
    public class EFGetUsersQuery : EFUseCase, IGetUsersQuery
    {
        public EFGetUsersQuery(ASPContext context):base(context)
        {

        }
        public int Id => 14;

        public string Name => "Get users";

        public PaginationResponse<UserDTO> Execute(BasePaginationSearch search)
        {
            var query = Context.Users.Include(x => x.Cars).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Username.Contains(search.Keyword) || x.Email.Contains(search.Keyword));
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


            var response = new PaginationResponse<UserDTO>();

            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new UserDTO { Username=x.Username,Email=x.Email,Phone=x.Phone, CarsCurrentlySelling=x.Cars.Count});
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;
            response.PageCount = (int)Math.Ceiling((float)response.TotalCount / search.PerPage.Value);

            return response;
        }
    }
}
