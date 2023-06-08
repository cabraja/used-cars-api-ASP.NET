using ASP5220.Application;
using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.DataAccess;
using ASP5220.Domain.Entities;
using ASP5220.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.UseCases.Commands
{
    public class EFEditCarCommand:EFUseCase, IEditCarCommand
    {
        private CreateCarValidator _validator;
        private IApplicationActor _actor;
        public EFEditCarCommand(ASPContext context, CreateCarValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _actor = actor;
        }
        public int Id => 8;

        public string Name => "Edit a car";

        public void Execute(EditCarDTO request)
        {
            if(request.Id < 1)
            {
                throw new ArgumentOutOfRangeException("Id auta nije validan.");
            }

            _validator.ValidateAndThrow(request);

            var car = Context.Cars
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .Include(x => x.Specifications).ThenInclude(x => x.SpecificationValue)
                .Include(x => x.Followers)
                .Include(x => x.Files)
                .Include(x => x.Make)
                .Include(x => x.User).ThenInclude(x => x.Cars)
                .FirstOrDefault(x => x.Id == request.Id);

            if (car == null)
            {
                throw new EntityNotFoundException("Automobil ne postoji u sistemu.");
            }

            if(_actor.Id != car.User.Id )
            {
                throw new UnauthorizedUseCaseException(this.Name, _actor.Username);
            }

            if(_actor.Role != "Admin")
            {
                if (_actor.Id != car.User.Id)
                {
                    throw new UnauthorizedUseCaseException(this.Name, _actor.Username);
                }

            }

            car.Price = request.Price;
            car.Model = request.Model;
            car.Variant = request.Variant;
            car.Mileage = request.Mileage;
            car.Power = request.Power;
            car.EngineCapacity = request.EngineCapacity;
            car.User = Context.Users.Find(_actor.Id);
            car.Make = Context.Makes.Find(request.MakeId);
            car.Files = request.Files.Select(x => new File { Path = x.Path }).ToList();
            car.Specifications = request.SpecificationValues.Select(x => new SpecificationCar
            {
                Car = car,
                SpecificationId = x.SpecificationId,
                SpecificationValueId = x.SpecificationValueId

            }).ToList();

            Context.SaveChanges();
        }
    }
}
