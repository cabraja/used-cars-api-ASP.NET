using ASP5220.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.UseCases
{
    public abstract class EFUseCase
    {
        protected ASPContext Context { get; }

        protected EFUseCase(ASPContext context)
        {
            Context = context;
        }
    }
}
