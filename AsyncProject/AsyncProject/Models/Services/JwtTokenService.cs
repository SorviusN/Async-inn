using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace AsyncProject.Models.Services
{
    public class JwtTokenService
    {
        private IConfiguration _configuration;
        private SignInManager<ApplicationUser> _signInManager;

        public JwtTokenService(IConfiguration config, SignInManager<ApplicationUser> manager)
        {
            _configuration = config;
            _signInManager = manager;
        }

        public async Task<string> GetTokenAsync(ApplicationUser user, TimeSpan expiresIn)
        {
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            if (principal == null) { return null; } // If we don't get a user, return null.

            var signingKey = GetSecurityKey(_configuration);
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expiresIn,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                claims: principal.Claims
            );

            // Takes our user claim + permissions, wraps it up into a ball and returns it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters // returns an instance of token validation
            {
                ValidateIssuerSigningKey = true,
                // This is main goal: Make sure the security key, which comes config is valid.
                IssuerSigningKey = GetSecurityKey(configuration), // this is the actual keypart.
                
                // for testing
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }

        // BOILERPLATE - THIS WILL ALWAYS WORK THE SAME AND TRIES TO AUTHENTICATE THE KEY
        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            // get the secret from app setting JSON
            var secret = configuration["JWT:Secret"];
            if (secret == null) { throw new InvalidOperationException("JWT: No Secret"); }

            // Encoding the secret into individual bytes (the way the token service wants it)
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(secretBytes);
        }
    }
}
