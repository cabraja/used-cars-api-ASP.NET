using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.DataAccess;
using ASP5220.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ASP5220.Domain.Entities;
using ASP5220.Application;

namespace ASP5220.Implementation.UseCases.Commands
{
    public class EFCreateCarCommand : EFUseCase, ICreateCarCommand
    {
        private CreateCarValidator _validator;
        private IApplicationActor _actor;
        public EFCreateCarCommand(ASPContext context, CreateCarValidator validator, IApplicationActor actor) :base(context)
        {
            _validator = validator;
            _actor = actor;
        }
        public int Id => 5;

        public string Name => "Create new car";

        public void Execute(CreateCarDTO request)
        {
            _validator.ValidateAndThrow(request);

            var car = new Car();
            car.Price = request.Price;
            car.Model = request.Model;
            car.Variant = request.Variant;
            car.Mileage = request.Mileage;
            car.Power = request.Power;
            car.EngineCapacity = request.EngineCapacity;
            car.User = Context.Users.Find(_actor.Id);
            car.Make = Context.Makes.Find(request.MakeId);
            car.Files = request.Files.Select(x => new File { Path=x.Path}).ToList();
            car.Specifications = request.SpecificationValues.Select(x => new SpecificationCar 
            { 
                Car=car,
                SpecificationId=x.SpecificationId,
                SpecificationValueId=x.SpecificationValueId

            }).ToList();

            Context.Cars.Add(car);
            Context.SaveChanges();
        }
    }
}
