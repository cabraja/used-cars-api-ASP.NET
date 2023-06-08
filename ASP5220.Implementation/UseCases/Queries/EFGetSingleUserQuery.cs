using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.DTO;
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
    public class EFGetSingleUserQuery : EFUseCase, IGetSingleUserQuery
    {
        public EFGetSingleUserQuery(ASPContext context):base(context)
        {

        }
        public int Id => 15;

        public string Name => "Get single user info";

        public UserDTO Execute(int search)
        {
            if (search < 1)
            {
                throw new ArgumentOutOfRangeException("Id korisnika nije validan.");
            }

            var entity = Context.Users.Include(x =>x.Cars ).FirstOrDefault(x => x.Id == search);

            if (entity == null)
            {
                throw new EntityNotFoundException("Korisnik ne postoji u sistemu.");
            }

            var user = new UserDTO
            {
                Username=entity.Username,
                Email=entity.Email,
                Phone=entity.Phone,
                CarsCurrentlySelling=entity.Cars.Count
            };

            return user;
        }
    }
}
