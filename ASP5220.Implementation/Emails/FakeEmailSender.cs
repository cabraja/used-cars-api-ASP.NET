using ASP5220.Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.Emails
{
    public class FakeEmailSender : IEmailSender
    {
        public void Send(MailMessage mail)
        {
            Console.WriteLine("Email sent to: " + mail.To);
        }
    }
}
