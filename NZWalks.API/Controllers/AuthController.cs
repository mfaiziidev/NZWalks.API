using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositries;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAuthRepository authRepository;

        public ITokenRepository TokenRepository { get; }

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, IAuthRepository authRepository)
        {
            this.userManager = userManager;
            TokenRepository = tokenRepository;
            this.authRepository = authRepository;
        }



        [HttpPost]
        [Route("/api/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var IdentityResult = await userManager.CreateAsync(IdentityUser, registerRequestDTO.Password);
            if (IdentityResult.Succeeded)
            {
                //Add Role to this user
                {
                    if(registerRequestDTO.Roles != null)
                    {
                        IdentityResult = await userManager.AddToRolesAsync(IdentityUser, registerRequestDTO.Roles);

                        if (IdentityResult.Succeeded)
                        {
                            return Ok("User has been registered! Please login");
                        }
                    }
                }
            }
            return BadRequest("Something wentwrong while creating User");
        }

        [HttpPost]
        [Route("/api/Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await authRepository.GetUserByUsernameAsync(loginRequestDTO.Username);

            if (user != null)
            {
                // Step 2: Validate the plain-text password (no hashing)
                if (user.Password == loginRequestDTO.Password)
                {
                    // Step 3: Get the user's role by RoleId
                    var roles = await authRepository.GetRoleByIdAsync(user.RoleId);  // Pass RoleId

                    if (roles != null)
                    {
                        // Step 4: Generate the JWT token with user details and role
                        var roleNames = roles.Select(role => role.Name).ToList();   // Extract role names as List<string>
                        var token = TokenRepository.CreateJWTToken(user, roleNames);

                        var response = new LoginResponseDTO
                        {
                            JwtToken = token,
                            UserId = user.Id,       
                            Name = user.Name,
                            Username = user.UserName,        
                            Email = user.Email,             
                            Roles = roleNames
                        };

                        return Ok(response);
                    }
                    else
                    {
                        return Unauthorized("User has an invalid role.");
                    }
                }
                else
                {
                    return BadRequest("Incorrect Username or Password.");
                }
            }
            else
            {
                return BadRequest("Incorrect Username or Password.");
            }
        }



        //[HttpPost]
        //[Route("/api/Login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        //{
        //    var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);
        //    if (user != null)
        //    {
        //        var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

        //        if(checkPasswordResult)
        //        {
        //            //Get Roles
        //            var roles = await userManager.GetRolesAsync(user);

        //            if (roles != null) 
        //            {
        //                var token = TokenRepository.CreateJWTToken(user, roles.ToList());
        //                var response = new LoginResponseDTO
        //                {
        //                    JwtToken = token
        //                };

        //                return Ok(response);
        //            }

        //        }
        //    }
        //    return BadRequest("Incorrect Username or Password");
        //}
    }
}
