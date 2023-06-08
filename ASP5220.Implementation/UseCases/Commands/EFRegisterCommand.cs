using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.DataAccess;
using ASP5220.Implementation.Validators;
using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP5220.Domain.Entities;
using ASP5220.Application.Emails;
using BCrypt.Net;

namespace ASP5220.Implementation.UseCases.Commands
{
    public class EFRegisterCommand : EFUseCase, IRegisterCommand
    {
        private RegisterValidator _validator;
        private IEmailSender _emailSender;
        public EFRegisterCommand(ASPContext context, RegisterValidator validator, IEmailSender emailSender) : base(context)
        {
            _validator = validator;
            _emailSender = emailSender;
        }
        public int Id => 1;
        public string Name => "Register user";

        public void Execute(RegisterDTO request)
        {
            _validator.ValidateAndThrow(request);
           

            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Phone = request.Phone
            };
            user.Role = Context.Roles.FirstOrDefault(x => x.Name.Equals("User"));

            Context.Users.Add(user);
            Context.SaveChanges();

            _emailSender.Send(new MailMessage { 
                To=request.Email,
                From="noreply",
                Title = "Successful registration.",
                Body ="Successful registration. Welcome!"
            });
        }
    }
}
