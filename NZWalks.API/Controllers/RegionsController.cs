﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomValidationAttribute;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositries.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("/api/GetAllRegions")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database using Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain models to DTOs
            //var regionDto = new List<RegionDTO>();
            //foreach (var data in regionsDomain)
            //{
            //    regionDto.Add(new RegionDTO()
            //    {
            //        id = data.id,
            //        Code = data.Code,
            //        Name = data.Name,
            //        RegionImageURL = data.RegionImageURL,
            //    });
            //}

            //Map Domain model to DTOs using automapper
            var regionDto = mapper.Map<List<RegionDTO>>(regionsDomain);

            //return DTOs
            return Ok(regionDto);
        }


        [HttpGet]
        [Route("/api/GetRegionsById/{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetRegionsById(int id)
        {
            var regionsDomain = await regionRepository.GetByIdAsync(id);

            if (regionsDomain is null)
            {
                return NotFound();
            }

            //var regionsDto = new RegionDTO
            //{
            //    id = regionsDomain.id,
            //    Code = regionsDomain.Code,
            //    Name = regionsDomain.Name,
            //    RegionImageURL = regionsDomain.RegionImageURL,
            //};

            //Mapping of DomainModel to DTO using autoMapper
            var regionsDto = mapper.Map<RegionDTO>(regionsDomain);
            return Ok(regionsDto);
        }


        [HttpPost]
        [Route("/api/AddRegions")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRegions([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Map or convert RegionDTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDTO.Code,
            //    Name = addRegionRequestDTO.Name,
            //    RegionImageURL = addRegionRequestDTO.RegionImageURL
            //};

            //Map or convert RegionDTO to Domain Model using Automapper
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

            //Use Domain model to Create Region using regionRepository
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);


            //Map Domain Model back to RegionDTO
            //var regionsDto = new RegionDTO
            //{
            //    id = regionDomainModel.id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL,
            //};

            //Map Domain Model back to RegionDTO
            var regionsDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionsById), new { id = regionsDto.id }, regionsDto);

        }


        [HttpPut]
        [Route("/api/UpdateRegion{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateRegions(int id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //Map DTO to Domain Model
            //var regionsDomainModel = new Region
            //{
            //    Code = updateRegionRequestDTO.Code,
            //    Name = updateRegionRequestDTO.Name,
            //    RegionImageURL = updateRegionRequestDTO.RegionImageURL,
            //};

            //Map DTO to Domain Model using AutoMapper
            var regionsDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

            //Check if region Exists using regionRepository
            var ExistingRegionFromDomainModel = await regionRepository.UpdateAsync(id, regionsDomainModel);

            if (ExistingRegionFromDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model back to RegionDTO
            //var regionsDto = new RegionDTO
            //{
            //    id = regionsDomainModel.id,
            //    Code = regionsDomainModel.Code,
            //    Name = regionsDomainModel.Name,
            //    RegionImageURL = regionsDomainModel.RegionImageURL,
            //};

            //Map Domain Model back to RegionDTO using AutoMapper
            var regionsDto = mapper.Map<RegionDTO>(ExistingRegionFromDomainModel);

            return Ok(regionsDto);
        }

        [HttpDelete]
        [Route("/api/DeleteRegion/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRegions(int id)
        {
            //Check if region Exists using regionRepository and delete directly
            var regionsDomainModel = await regionRepository.DeleteAsync(id);

            //Map Domain Model back to RegionDTO
            //var regionsDto = new RegionDTO
            //{
            //    id = regionsDomainModel.id,
            //    Code = regionsDomainModel.Code,
            //    Name = regionsDomainModel.Name,
            //    RegionImageURL = regionsDomainModel.RegionImageURL,
            //};

            //Map Domain Model back to RegionDTO using AutoMapper
            var regionsDto = mapper.Map<RegionDTO>(regionsDomainModel);

            return Ok(regionsDto);
        }
    }
}
