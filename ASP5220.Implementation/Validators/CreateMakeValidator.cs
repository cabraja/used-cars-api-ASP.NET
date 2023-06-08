using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.DataAccess;
using FluentValidation;

namespace ASP5220.Implementation.Validators
{
    public class CreateMakeValidator:AbstractValidator<CreateMakeDTO>
    {
        private ASPContext _context;
        public CreateMakeValidator(ASPContext context)
        {
            _context = context;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Morate uneti naziv marke.")
                .MinimumLength(2).WithMessage("Ime marke mora imati bar 2 karaktera")
                .Must(x => !_context.Makes.Any(m => x == m.Name)).WithMessage("Ista marka već postoji u bazi.");
        }
    }
}
