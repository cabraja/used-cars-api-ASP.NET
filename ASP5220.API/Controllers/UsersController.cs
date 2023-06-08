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
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UsersController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        // GET: api/<UsersController>
        /// <summary>
        /// Gets infomration about users (only admins)
        /// </summary>
        /// <returns>Info about users</returns>
        /// <response code="200">Data fetched successfuly.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="404">user does not exist in the database.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] BasePaginationSearch search,[FromServices] IGetUsersQuery query)
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

        // GET api/<UsersController>/5
        /// <summary>
        /// Gets full information about a user (only admins)
        /// </summary>
        /// <returns>Detailed info about user</returns>
        /// <response code="200">Data fetched successfuly.</response>
        /// <response code="400">ID is out of range.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="404">user does not exist in the database.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSingleUserQuery query)
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

    }
}
