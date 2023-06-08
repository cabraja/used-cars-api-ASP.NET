using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.Emails
{
    public interface IEmailSender
    {
        void Send(MailMessage mail);
    }
}
