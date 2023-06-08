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
    public class EFDeleteUserCommand : EFUseCase, IDeleteUserCommand
    {
        private IApplicationActor _actor;
        public EFDeleteUserCommand(ASPContext context, IApplicationActor actor):base(context)
        {
            _actor = actor;
        }
        public int Id => 13;

        public string Name => "Delete a user";

        public void Execute(int request)
        {
            if (request < 1)
            {
                throw new ArgumentOutOfRangeException("Id korisnika nije validan.");
            }

            var user = Context.Users.FirstOrDefault(x => x.Id == request);

            if (user == null)
            {
                throw new EntityNotFoundException("Korisnik ne postoji u sistemu.");
            }

            if (_actor.Role != "Admin")
            {
                if (_actor.Id != user.Id)
                {
                    throw new UnauthorizedUseCaseException(this.Name, _actor.Username);
                }

            }

            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
