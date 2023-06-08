using ASP5220.Application.UseCases.DTO;
using ASP5220.Application.UseCases.Queries;
using ASP5220.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP5220.Application.Exceptions;

namespace ASP5220.Implementation.UseCases.Queries
{
    public class EFGetSingleCarQuery : EFUseCase, IGetSingleCarQuery
    {
        public EFGetSingleCarQuery(ASPContext context):base(context)
        {

        }
        public int Id => 7;

        public string Name => "Get info about one car";

        public SingleCarDTO Execute(int search)
        {
            if(search < 1)
            {
                throw new ArgumentOutOfRangeException("Id auta nije validan.");
            }

            var entity = Context.Cars
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .Include(x => x.Specifications).ThenInclude(x => x.SpecificationValue)
                .Include(x => x.Followers)
                .Include(x => x.Files)
                .Include(x => x.Make)
                .Include(x => x.User).ThenInclude(x => x.Cars)
                .FirstOrDefault(x => x.Id == search);

            if (entity == null)
            {
                throw new EntityNotFoundException("Automobil ne postoji u sistemu.");
            }

            var car = new SingleCarDTO
            {
                Id = entity.Id,
                Price = entity.Price,
                Model = entity.Model,
                Variant = entity.Variant,
                Mileage = entity.Mileage,
                Power = entity.Power,
                EngineCapacity = entity.EngineCapacity,
                Make = entity.Make.Name,
                Seller = new UserDTO {
                    Username = entity.User.Username,
                    Email = entity.User.Email,
                    Phone = entity.User.Phone,
                    CarsCurrentlySelling = entity.User.Cars.Count
                },
                Files = entity.Files.Select(x => new FileDTO { Path = x.Path }).ToList(),
                Specifications = entity.Specifications.Select(x => new SpecificationAndValueDTO {
                    SpecificationName = x.Specification.Name, SpecificationValue = x.SpecificationValue.Value
                }).ToList(),
                FollowersCount=entity.Followers.Count
            };

            return car;
        }
    }
}
