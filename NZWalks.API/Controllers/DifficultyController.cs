﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("/api/AddDifficulty")]
        public async Task<IActionResult> Create([FromBody] AddDifficultyRequestDTO addDifficultyRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var difficultyDomainModel = mapper.Map<Difficulty>(addDifficultyRequestDTO);
                var difficultyData = await difficultyRepository.CreateAsync(difficultyDomainModel);
                var DifficultyDTO = mapper.Map<DifficultyDTO>(difficultyData);
                return Ok(DifficultyDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("/api/GetAllDifficulty")]
        public async Task<IActionResult> GetAll()
        {
            var DifficultyDomainModel = await difficultyRepository.GetAllAsync();
            var difficultyDTO = mapper.Map<List<DifficultyDTO>>(DifficultyDomainModel);            
            return Ok(difficultyDTO);
        }

        [HttpGet]
        [Route("/api/GetDifficultyByID{id}")]
        public async Task<IActionResult> GetDifficultyById(Guid id)
        {
            var DifficultyDomainModel = await difficultyRepository.GetDifficultyByIdAsync(id);
            var difficultyDTO = mapper.Map<DifficultyDTO>(DifficultyDomainModel);
            return Ok(difficultyDTO);
        }

        [HttpPut]
        [Route("/api/UpdateDifficulty/{id}")]
        public async Task<IActionResult> UpdateDifficulty(Guid id, [FromBody] UpdateDifficultyRequestDTO updateDifficultyRequestDTO)
        {
            if (ModelState.IsValid) 
            {
                var DifficultyDomainModel = mapper.Map<Difficulty>(updateDifficultyRequestDTO);

                var UpdatedDifficulty = await difficultyRepository.UpdateAsync(DifficultyDomainModel, id);
                if (UpdatedDifficulty == null)
                { return NotFound(); }

                var difficultyDTO = mapper.Map<DifficultyDTO>(UpdatedDifficulty);
                return Ok(difficultyDTO);
            }
            else {return BadRequest(ModelState); }
        }

        [HttpDelete]
        [Route("/api/DeleteDifficulty/{id}")]
        public async Task<IActionResult> DeleteDifficulty(Guid id)
        {
            var DeletedDifficulty = await difficultyRepository.DeleteAsync(id);
            var difficultyDTO = mapper.Map<DifficultyDTO>(DeletedDifficulty);
            return Ok(difficultyDTO);
        }
    }
}
