using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.Application.UseCases.DTO.Searches;
using ASP5220.Application.UseCases.Queries;
using ASP5220.Implementation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP5220.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private UseCaseHandler _handler;

        public CarsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<CarsController>
        /// <summary>
        /// Gets basic information about cars (paginated)
        /// </summary>
        /// <returns>Paginated cars</returns>
        /// <response code="200">Data fetched successfuly.</response>
        /// <response code="500">Unexpected server error.</response>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery] BasePaginationSearch search, [FromServices] IGetCarsQuery query)
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

        // GET api/<CarsController>/5
        /// <summary>
        /// Gets full information about a car
        /// </summary>
        /// <returns>Detailed info about car</returns>
        /// <response code="200">Data fetched successfuly.</response>
        /// <response code="400">ID is out of range.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="404">Car does not exist in the database.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id, [FromServices] IGetSingleCarQuery query)
        {
            try
            {
                return Ok(_handler.HandleQuery(query, id));
            }
            catch (UnauthorizedUseCaseException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentOutOfRangeException)
            {
                return StatusCode(400);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/<CarsController>
        /// <summary>
        /// Creates new car
        /// </summary>
        /// <returns>Void</returns>
        /// <response code="201">Car created successfuly.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="422">Data passed is invalid.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateCarDTO make, [FromServices] ICreateCarCommand command)
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
                return UnprocessableEntity(ve.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/<CarsController>/5
        /// <summary>
        /// Edits a car
        /// </summary>
        /// <returns>Void</returns>
        /// <response code="204">Car edited successfuly.</response>
        /// <response code="400">ID is out of range.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="404">Car does not exist in the database.</response>
        /// <response code="422">Data passed is invalid.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPut("{id}")]
        [Authorize]

        public IActionResult Put(int id, [FromBody] EditCarDTO dto, [FromServices] IEditCarCommand command)
        {
            try
            {
                dto.Id = id;
                _handler.HandleCommand(command, dto);
                return StatusCode(204);
            }
            catch (UnauthorizedUseCaseException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentOutOfRangeException)
            {
                return StatusCode(400);
            }
            catch (ValidationException ve)
            {
                return UnprocessableEntity(ve.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<CarsController>/5
        /// <summary>
        /// Deletes a car
        /// </summary>
        /// <returns>Void</returns>
        /// <response code="204">Car deleted successfuly.</response>
        /// <response code="400">ID is out of range.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="404">Car does not exist in the database.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult Delete(int id,[FromServices] IDeleteCarCommand command)
        {
            try
            {
                _handler.HandleCommand(command, id);
                return StatusCode(204);
            }
            catch (UnauthorizedUseCaseException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentOutOfRangeException)
            {
                return StatusCode(400);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
