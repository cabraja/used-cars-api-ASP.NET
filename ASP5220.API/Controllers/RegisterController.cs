using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO.Inserts;
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
    public class RegisterController : ControllerBase
    {
        private UseCaseHandler _handler;

        public RegisterController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // POST api/<RegisterController>
        /// <summary>
        /// Registers new user
        /// </summary>
        /// <returns>Void</returns>
        /// <response code="201">User created successfuly.</response>
        /// <response code="422">Data about user is not valid.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterDTO dto,[FromServices] IRegisterCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                return StatusCode(201);
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

        // DELETE api/<RegisterController>/5
        /// <summary>
        /// Deletes user from database.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>Void</returns>
        /// <response code="204">User deleted successfuly.</response>
        /// <response code="400">ID is not in valid range.</response>
        /// <response code="404">User does not exist in database.</response>
         /// <response code="500">Unexpected server error.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand command)
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
                return NotFound(ex.Message);
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
