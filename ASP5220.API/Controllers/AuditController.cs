using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.DTO.Searches;
using ASP5220.Application.UseCases.Queries;
using ASP5220.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP5220.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private UseCaseHandler _handler;

        public AuditController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<AuditController>
        /// <summary>
        /// Gets paginated audit logs (only admins)
        /// </summary>
        /// <returns>Paginated audit logs</returns>
        /// <response code="200">Data fetched successfuly.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] AuditLogSearchDTO search, [FromServices] IGetAuditLogsQuery query)
        {
            try
            {
                return Ok(_handler.HandleQuery(query, search));
            }
            catch (UnauthorizedUseCaseException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        
    }
}
