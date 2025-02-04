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
    
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DifficultyController> logger;

        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper mapper, ILogger<DifficultyController> logger)
        {
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [Route("/api/AddDifficulty")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddDifficultyRequestDTO addDifficultyRequestDTO)
        {
            var difficultyDomainModel = mapper.Map<Difficulty>(addDifficultyRequestDTO);
            var difficultyData = await difficultyRepository.CreateAsync(difficultyDomainModel);
            var DifficultyDTO = mapper.Map<DifficultyDTO>(difficultyData);
            return Ok(DifficultyDTO);
        }

        [HttpGet]
        [Route("/api/GetAllDifficulty")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Get All Difficulties action method Invoked"); // Console Logging
            var DifficultyDomainModel = await difficultyRepository.GetAllAsync();
            logger.LogInformation($"Data coming from DB is: {JsonSerializer.Serialize(DifficultyDomainModel)}"); // Console Logging
            var difficultyDTO = mapper.Map<List<DifficultyDTO>>(DifficultyDomainModel);            
            return Ok(difficultyDTO);
        }

        [HttpGet]
        [Route("/api/GetDifficultyByID{id}")]
        [Authorize(Roles = "Admin, User")]

        public async Task<IActionResult> GetDifficultyById(int id)
        {
            var DifficultyDomainModel = await difficultyRepository.GetDifficultyByIdAsync(id);
            var difficultyDTO = mapper.Map<DifficultyDTO>(DifficultyDomainModel);
            return Ok(difficultyDTO);
        }

        [HttpPut]
        [Route("/api/UpdateDifficulty/{id}")]
        [ValidateModel]
        [Authorize(Roles = ("Admin"))]

        public async Task<IActionResult> UpdateDifficulty(int id, [FromBody] UpdateDifficultyRequestDTO updateDifficultyRequestDTO)
        {
            var DifficultyDomainModel = mapper.Map<Difficulty>(updateDifficultyRequestDTO);

            var UpdatedDifficulty = await difficultyRepository.UpdateAsync(DifficultyDomainModel, id);
            if (UpdatedDifficulty == null)
            { return NotFound(); }

            var difficultyDTO = mapper.Map<DifficultyDTO>(UpdatedDifficulty);
            return Ok(difficultyDTO);
        }

        [HttpDelete]
        [Route("/api/DeleteDifficulty/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteDifficulty(int id)
        {
            var DeletedDifficulty = await difficultyRepository.DeleteAsync(id);
            var difficultyDTO = mapper.Map<DifficultyDTO>(DeletedDifficulty);
            return Ok(difficultyDTO);
        }
    }
}
