using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomValidationAttribute;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositries.IRepository;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("/api/AddRole")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddRoleRequestDTO addRoleRequestDTO)
        {
            var roleDomainModel = mapper.Map<Role>(addRoleRequestDTO);
            var roleData = await roleRepository.CreateAsync(roleDomainModel);
            var RoleDTO = mapper.Map<RoleDTO>(roleData);
            return Ok(RoleDTO); 
        }

        [HttpGet]
        [Route("/api/GetAllRole")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAll()
        {
            //logger.LogInformation("Get All Difficulties action method Invoked"); // Console Logging
            var RoleDomainModel = await roleRepository.GetAllAsync();
            //logger.LogInformation($"Data coming from DB is: {JsonSerializer.Serialize(DifficultyDomainModel)}"); // Console Logging
            var roleDTO = mapper.Map<List<RoleDTO>>(RoleDomainModel);
            return Ok(roleDTO);
        }
    }
}
