using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.Emails
{
    public class MailMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
