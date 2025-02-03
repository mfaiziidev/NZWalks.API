using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("/api/AddUser")]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDTO addUserRequestDTO)
        {
            var UserDomainModel = mapper.Map<User>(addUserRequestDTO);
            await userRepository.CreateAsync(UserDomainModel);
            var UserDTO = mapper.Map<UserDTO>(UserDomainModel);

            return Ok(UserDTO);
        }

        [HttpGet]
        [Route("/api/GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var UserDomainModel = await userRepository.GetAllAsync();
            var UserDTO = mapper.Map<List<UserDTO>>(UserDomainModel);

            return Ok(UserDTO);
        }

        [HttpGet]
        [Route("/api/GetUserById/{id}")]
        public async Task<IActionResult> GetAllUser(int id)
        {
            var UserDomainModel = await userRepository.GetByIdAsync(id);
            var UserDTO = mapper.Map<UserDTO>(UserDomainModel);

            return Ok(UserDTO);
        }

        [HttpPut]
        [Route("/api/UpdateUserById/{id}")]
        public async Task<IActionResult> UpdateUserById(int id, [FromBody] UpdateUserRequestDTO updateUserRequestDTO)
        {
            var UserDomainModel = mapper.Map<User>(updateUserRequestDTO);
            var UserDomainModelData = await userRepository.UpdateUserAsync(id, UserDomainModel);
            var UserDTO = mapper.Map<UserDTO>(UserDomainModelData);

            return Ok(UserDTO);
        }

        [HttpDelete]
        [Route("/api/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var UserDomainModelData = await userRepository.DeleteUserAsync(id);
            var UserDTO = mapper.Map<UserDTO>(UserDomainModelData);

            return Ok(UserDTO);
        }
    }
}
