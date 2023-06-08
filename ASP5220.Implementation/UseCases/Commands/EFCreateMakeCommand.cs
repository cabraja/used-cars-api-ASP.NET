using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.DataAccess;
using ASP5220.Domain.Entities;
using ASP5220.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.UseCases.Commands
{
    public class EFCreateMakeCommand : EFUseCase, ICreateMakeCommand
    {
        private CreateMakeValidator _validator;
        public EFCreateMakeCommand(ASPContext context, CreateMakeValidator validator) :base(context)
        {
            _validator = validator;
        }
        public int Id => 2;
        public string Name => "Create new make";

        public void Execute(CreateMakeDTO request)
        {

            _validator.ValidateAndThrow(request);

            var make = new Make();
            make.Name = request.Name;

            Context.Makes.Add(make);
            Context.SaveChanges();
        }
    }
}
