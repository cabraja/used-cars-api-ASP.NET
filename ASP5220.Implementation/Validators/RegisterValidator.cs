using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.DataAccess;

namespace ASP5220.Implementation.Validators
{
    public class RegisterValidator:AbstractValidator<RegisterDTO>
    {
        private ASPContext _context;
        public RegisterValidator(ASPContext context)
        {
            _context = context;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Username).NotEmpty().WithMessage("Morate uneti username.")
                .MinimumLength(3).WithMessage("Minimalan broj karaktera je 3.")
                .MaximumLength(24).WithMessage("Maksimalan broj karaktera je 24.")
                .Matches("^(?=.{3,24}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").WithMessage("Username je u neispravnom formatu.")
                .Must(x => !_context.Users.Any(u => u.Username == x)).WithMessage("Username vec postoji u sistemu.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Morate uneti lozinku.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage("Lozinka je u nesipravnom formatu.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Morate uneti email adresu.")
                .EmailAddress().WithMessage("Email adresa nije u ispravnom formatu.")
                .Must(x => !_context.Users.Any(u => u.Email == x)).WithMessage("Email vec postoji u sistemu.");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Morate uneti broj telefona.")
                .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").WithMessage("Unesite validan broj telefona.");
        }
    }
}
