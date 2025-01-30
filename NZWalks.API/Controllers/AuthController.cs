using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
    }
}
