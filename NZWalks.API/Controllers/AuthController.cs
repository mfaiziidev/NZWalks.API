using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public ITokenRepository TokenRepository { get; }

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            TokenRepository = tokenRepository;
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
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if(checkPasswordResult)
                {
                    //Get Roles
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null) 
                    {
                        var token = TokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = token
                        };

                        return Ok(response);
                    }
                    
                }
            }
            return BadRequest("Incorrect Username or Password");
        }
    }
}
