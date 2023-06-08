using ASP5220.Application.Exceptions;
using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO.Inserts;
using ASP5220.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP5220.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private UseCaseHandler _handler;

        public FollowController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // POST api/<FollowController>
        /// <summary>
        /// Mark car as followed
        /// </summary>
        /// <returns>Void</returns>
        /// <response code="201">Car followed successfuly.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="422">Car is already followed by the user or the car does not exist.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] FollowCarDTO dto,[FromServices] IFollowCarCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
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
            catch(EntityExistsException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<FollowController>/5
        /// <summary>
        /// Unfollows a car
        /// </summary>
        /// <returns>Void</returns>
        /// <response code="204">Car unfollowed successfuly.</response>
        /// <response code="400">ID is out of range.</response>
        /// <response code="401">User is not allowed to access this data.</response>
        /// <response code="404">Car does not exist in the database.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IUnfollowCarCommand command)
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
