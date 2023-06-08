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
   public class CreateCarValidator:AbstractValidator<CreateCarDTO>
    {
        private ASPContext _context;
        public CreateCarValidator(ASPContext context)
        {
            _context = context;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Price).NotNull().WithMessage("Morate uneti cenu.")
                .GreaterThan(0).WithMessage("Cena mora biti veca od 0.");

            RuleFor(x => x.Model).NotNull().WithMessage("Morate uneti ime modela")
                .NotEmpty().WithMessage("Ime modela ne sme biti prazan string.");

            RuleFor(x => x.Variant).NotNull().WithMessage("Morate uneti varijantu/seriju.")
                .NotEmpty().WithMessage("Ime varijante ne sme biti prazan string.");

            RuleFor(x => x.EngineCapacity).NotNull().WithMessage("Morate uneti kubikazu.")
                .GreaterThan(0).WithMessage("Kubikaza mora biti veca od 0.");

            RuleFor(x => x.Mileage).NotNull().WithMessage("Morate uneti kolometrazu.")
                .GreaterThan(-1).WithMessage("Kilometraza ne moze biti negativna.");

            RuleFor(x => x.Power).NotNull().WithMessage("Morate uneti snagu u HP(konjska snaga).")
                .GreaterThan(0).WithMessage("Snaga mora biti veca od 0.");

            RuleFor(x => x.MakeId).NotNull().WithMessage("Morate izabrati marku.")
                .GreaterThan(0).WithMessage("Id marke nije validan")
                .Must(x => _context.Makes.Any(m => m.Id == x)).WithMessage("Id marke ne postoji u sistemu.");

            RuleFor(x => x.SpecificationValues).NotNull().WithMessage("Morate izabrati specifikacije i njihove vrednosti.")
                .Must(x => x.Count() == _context.Specifications.Count()).WithMessage("Niste izabrali sve specifikacije.")
                .Must(x => !x.GroupBy(svm => svm.SpecificationId).Any(group => group.Count() > 1))
                .WithMessage("Za neku od specifikacija ste dodali vise od jedne vrednosti, a sme biti prosledjena samo jedna.");

            RuleForEach(x => x.SpecificationValues).Must(x => _context.Specifications.Any(spec => spec.Id == x.SpecificationId)).WithMessage("Barem jedna od ponudjenih specifikacija ne postoji")
                .Must(x => _context.SpecificationValues.Any(sv => sv.Id == x.SpecificationValueId)).WithMessage("Barem jedna od vrednosti specifikacija ne postoji u sistemu.");

            RuleFor(x => x.Files).NotNull().WithMessage("Morate uneti bar jednu sliku automobila");

            RuleForEach(x => x.Files).Must(x => !string.IsNullOrEmpty(x.Path)).WithMessage("URL slike ne sme biti prazan string.");
        }
    }
}
