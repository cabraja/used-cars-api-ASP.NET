using ASP5220.Application;
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
using ASP5220.Application.Exceptions;
using ASP5220.Domain.Entities;

namespace ASP5220.Implementation.UseCases.Commands
{
    public class EFFollowCarCommand : EFUseCase, IFollowCarCommand
    {
        private FollowCarValidator _validator;
        private IApplicationActor _actor;
        public EFFollowCarCommand(ASPContext context, FollowCarValidator validator, IApplicationActor actor):base(context)
        {
            _validator = validator;
            _actor = actor;
        }
        public int Id => 11;

        public string Name => "Follow a car";

        public void Execute(FollowCarDTO request)
        {
            _validator.ValidateAndThrow(request);

            var car = Context.Cars.Find(request.CarId);

            // Trenutno ulogovani korisnik
            var user = Context.Users.Find(_actor.Id);

            if(user == null)
            {
                throw new EntityNotFoundException("Korisnik ne postoji u sistemu.");
            }

            var carUser = new CarUser { UserId=user.Id, CarId=car.Id, Car=car, User=user};

            if(Context.CarFollowers.Any(x =>  x.CarId == carUser.CarId && x.UserId == carUser.UserId))
            {
                throw new EntityExistsException("Vec pratite ovaj oglas");
            }

            car.Followers.Add(carUser);
            Context.SaveChanges();
        }
    }
}
