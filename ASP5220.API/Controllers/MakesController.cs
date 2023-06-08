using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.Application.UseCases.DTO.Searches;
using ASP5220.Application.UseCases.Queries;
using ASP5220.Implementation;
using ASP5220.Implementation.Validators;
using FluentValidation;
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
    public class MakesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public MakesController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        // GET: api/<MakesController>
        /// <summary>
        /// Gets paginated car makes
        /// </summary>
        /// <returns>Paginated info about makes</returns>
        /// <response code="200">Data fetched successfuly.</response>
        /// <response code="500">Unexpected server error.</response>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery]BasePaginationSearch search, [FromServices] IGetMakesQuery query)
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

        // POST: api/<MakesController>
        /// <summary>
        /// Create a make
        /// </summary>
        /// <returns>Void</returns>
        /// <response code="201">Make created successfuly.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="422">Data passed is invalid.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateMakeDTO make,[FromServices] ICreateMakeCommand command)
        {
            try
            {
                _handler.HandleCommand(command, make);
                return StatusCode(201);
            }
            catch (UnauthorizedUseCaseException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ValidationException ve)
            {
                return UnprocessableEntity(ve.Errors.Select(x => new { x.PropertyName,x.ErrorMessage}).ToList());
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
            
        }


    }
}
