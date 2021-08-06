using AsyncProject.Models.DTO;
using AsyncProject.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Controller
{
    // Basic API for user controller.
    // We need to wire up the dependency.
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser _userService;

        // Controller is now set up to use the service via DI (dependency injection)
        public UsersController(IUser service)
        {
            _userService = service;
        }

        // Routes

        [HttpPost("Register)")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO data)
        {
            // Passing in the model state - if there is a problem with the model state we will change it
            // to invalid and have that reflect in the code.
            var user = await _userService.Register(data, this.ModelState);
            if (ModelState.IsValid) return user;

            // Show all of the error messages that exist.
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        // Creating a login session for the user.
        public async Task<ActionResult<UserDTO>> Login(LoginDTO data)
        {
            var userLogin = await _userService.Login(data.Username, data.Password);

            // if unauthorized is null, let them know.
            if (userLogin == null) return Unauthorized();

            return userLogin;
        }
    }

}
