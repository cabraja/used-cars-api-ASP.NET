using ASP5220.Application.UseCases;
using ASP5220.DataAccess;
using ASP5220.Domain.Entities;
using ASP5220.Implementation.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP5220.Implementation.Logging
{
    public class UseCaseLogger : EFUseCase, IUseCaseLogger
    {
        public UseCaseLogger(ASPContext context):base(context)
        {

        }
        public void Log(UseCaseLog log)
        {
            var auditLog = new AuditLog { 
                UseCaseName=log.UseCaseName,
                Username=log.User,
                ExecutionTime=log.ExecutionTime,
                IsAuthorized=log.IsAuthorized,
                Data=log.Data
            };

            Context.AuditLogs.Add(auditLog);
            Context.SaveChanges();
        }
    }
}
