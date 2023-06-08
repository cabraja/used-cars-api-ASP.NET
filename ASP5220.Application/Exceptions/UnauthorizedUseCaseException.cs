using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.Exceptions
{
    public class UnauthorizedUseCaseException:Exception
    {
        public UnauthorizedUseCaseException(string useCase,string user):base("Nemate dozvolu da izvrsite ovu operaciju.")
        {

        }
    }
}
