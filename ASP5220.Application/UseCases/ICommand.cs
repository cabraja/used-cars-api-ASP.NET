using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Application.UseCases
{
    public interface ICommand<TRequest>:IUseCase
    {
        void Execute(TRequest request);
    }
}
