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
   public class FollowCarValidator:AbstractValidator<FollowCarDTO>
    {
        private ASPContext _context;

        public FollowCarValidator(ASPContext context)
        {
            _context = context;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CarId).NotNull().WithMessage("Morate izabrati id vozila.")
                .GreaterThan(0).WithMessage("Id vozila ne sme biti manji od 1.")
                .Must(x => context.Cars.Any(c => c.Id == x)).WithMessage("Ovo vozilo ne postoji u sistemu.");
        }
    }
}
