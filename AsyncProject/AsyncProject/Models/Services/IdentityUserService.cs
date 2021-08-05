using AsyncProject.Models.DTO;
using AsyncProject.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models.Services
{
    public class IdentityUserService : IUser
    {

        // Allows us to get into the specifics of identity with the user.
        private UserManager<ApplicationUser> userManager;

        //dependency injection - injecting userManager (application user model)
        public IdentityUserService(UserManager<ApplicationUser> manager)
        {
            userManager = manager;
        }

        public async Task<UserDTO> Login(string username, string password)
        {
            // Find the user inside of the database by name.
            var user = await userManager.FindByNameAsync(username);

            // Check if the password matches what is inside the database.
            if ( await userManager.CheckPasswordAsync(user, password))
            {
                // if it is, return the information from the user in the form of a DTO
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }

            return null;
        }

        public async Task<UserDTO> Register(RegisterUserDTO data, ModelStateDictionary modelState)
        {
            // Apply all data that is entered from RegisteredUserDTO
            // Assign it to a new model of application user (implements the identity interface stuff)
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            // Create a user to store inside of user manager with the given name and password.
            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                // return new DTO from ApplicationUser that was created 
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }

            // Check if any of the errors exist within the model state that we passed in.
            // This will return an error if the state is changed at any point within the ApplicationUser
            foreach(var error in result.Errors)
            {
                // Model state will be an object, where the key will be password, email, userName
                // and the error messages will be the error messages.
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";

                // Add the errors to the model, whichever ones were present.
                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}
