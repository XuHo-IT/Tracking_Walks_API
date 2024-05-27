using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Model.DTO;
using NZWalk.API.Repositories;
using NZWalk.API.Respositories;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;
        private readonly ITokenrepository tokenrepository;

        public AuthController(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, ITokenrepository tokenrepository)
        {
            this.userManager = userManager;
            this.tokenrepository = tokenrepository;
        }

        // POST: Api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName,
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {

                //Add role to this User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                   identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Pleaase login");
                    }
                }
              
            }
            return BadRequest("Something went wrong");


        }

        //POST: api/Auth/Login
        [HttpPost("login")]
        public IActionResult Login(LoginRequestDTO loginRequest)
        {
            // Validate loginRequest, authenticate user, and retrieve roles
            // For demonstration purposes, let's assume user authentication is successful and roles are retrieved
            var user = new IdentityUser { Email = loginRequest.Username };
            var roles = new List<string> { "Role1", "Role2" };

            // Generate JWT token
            var jwtToken = tokenrepository.CreateJWTToken(user, roles);

            // Create the response DTO
            var response = new LoginResponseDTO
            {
                JwtToken = jwtToken
            };

            // Return the response as JSON
            return Ok(response);
        }
    }
           
    }

