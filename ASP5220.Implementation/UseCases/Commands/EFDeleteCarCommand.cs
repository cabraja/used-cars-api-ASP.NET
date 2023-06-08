using ASP5220.Application;
using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.Commands;
using ASP5220.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.UseCases.Commands
{
    public class EFDeleteCarCommand : EFUseCase, IDeleteCarCommand
    {
        private IApplicationActor _actor;
        public EFDeleteCarCommand(ASPContext context, IApplicationActor actor):base(context)
        {
            _actor = actor;
        }
        public int Id => 9;

        public string Name => "Delete car";

        public void Execute(int request)
        {
            if (request < 1)
            {
                throw new ArgumentOutOfRangeException("Id auta nije validan.");
            }

            var car = Context.Cars.Include(x => x.User).FirstOrDefault(x => x.Id == request);

            if(car == null)
            {
                throw new EntityNotFoundException("Automobil ne postoji u sistemu.");
            }

            if (_actor.Role != "Admin")
            {
                if (_actor.Id != car.User.Id)
                {
                    throw new UnauthorizedUseCaseException(this.Name, _actor.Username);
                }

            }

            Context.Remove(car);
            Context.SaveChanges();
        }
    }
}
