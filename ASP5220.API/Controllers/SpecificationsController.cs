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
    public class SpecificationsController : ControllerBase
    {

        private UseCaseHandler _handler;

        public SpecificationsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<SpecificationsController>
        /// <summary>
        /// Gets data about specifications
        /// </summary>
        /// <returns>Detailed info about specifications</returns>
        /// <response code="200">Data fetched successfuly.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] BaseSearchDTO search, [FromServices] IGetSpecificationsQuery query)
        {
            try
            {
                return Ok(_handler.HandleQuery(query, search));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        
    }
}
