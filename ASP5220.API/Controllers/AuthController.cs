using ASP5220.API.Core;
using ASP5220.DataAccess;
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
    public class AuthController : ControllerBase
    {

        private readonly JwtManager _manager;

        public AuthController(JwtManager manager)
        {
            _manager = manager;
        }

        // POST api/<AuthController>
        /// <summary>
        /// Performs login authentication
        /// </summary>
        /// <returns>JWT Token</returns>
        /// <response code="200">Login was successful.</response>
        /// <response code="401">Credentials were not correct.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest request,
                         [FromServices] ASPContext context)
        {
            try
            {
                string token = _manager.MakeToken(request.Email, request.Password);

                return Ok(new { token });
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }


    }
}
