using ASP5220.Application;
using ASP5220.Application.Logging;
using ASP5220.Application.UseCases;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP5220.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ASP5220.Implementation
{
    public class UseCaseHandler
    {
        private IExceptionLogger _logger;
        private IApplicationActor _user;
        private IUseCaseLogger _useCaseLogger;

        public UseCaseHandler(IExceptionLogger logger, IApplicationActor user, IUseCaseLogger useCaseLogger)
        {
            _logger = logger;
            _user = user;
            _useCaseLogger = useCaseLogger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                var log = new UseCaseLog {
                    UseCaseName = command.Name,
                    User = _user.Username,
                    UserId = _user.Id,
                    ExecutionTime = DateTime.UtcNow,
                    Data = JsonConvert.SerializeObject(data),
                    IsAuthorized = _user.AllowedUseCases.Contains(command.Id)
                };

                _useCaseLogger.Log(log);

                if (!log.IsAuthorized)
                {
                    throw new UnauthorizedUseCaseException(log.UseCaseName,log.User);
                }


                command.Execute(data);
            }
            catch(UnauthorizedUseCaseException ex)
            {
                throw;
            }
            catch(DbUpdateException ex)
            {
                throw;
            }
            catch (EntityExistsException)
            {
                throw;
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public TResponse HandleQuery<TRequest, TResponse>(IQuery<TRequest,TResponse> query, TRequest data) where TResponse : class
        {
            try
            {
                var log = new UseCaseLog
                {
                    UseCaseName = query.Name,
                    User = _user.Username,
                    UserId = _user.Id,
                    ExecutionTime = DateTime.UtcNow,
                    Data = JsonConvert.SerializeObject(data),
                    IsAuthorized = _user.AllowedUseCases.Contains(query.Id)
                };

                _useCaseLogger.Log(log);

                if (!log.IsAuthorized)
                {
                    throw new UnauthorizedUseCaseException(log.UseCaseName, log.User);
                }

                var response = query.Execute(data);
                return response;
            }
            catch(ArgumentOutOfRangeException ex)
            {
                throw;
            }
            catch(EntityNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
    }
}
