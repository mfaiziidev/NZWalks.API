using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from Database using Domain models
            var regionsDomain = dbContext.Regions.ToList();

            //Map Domain models to DTOs
            var regionDto = new List<RegionDTO>();
            foreach (var data in regionsDomain)
            {
                regionDto.Add(new RegionDTO()
                {
                    id = data.id,
                    Code = data.Code,
                    Name = data.Name,
                    RegionImageURL = data.RegionImageURL,
                });

            }

            //return DTOs
            return Ok(regionDto);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRegionsById(Guid id)
        {
            var regionsDomain = dbContext.Regions.Find(id);

            if(regionsDomain is null)
            {
                return NotFound();
            }

            var regionsDto = new RegionDTO
            {
                id = regionsDomain.id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageURL = regionsDomain.RegionImageURL,
            };
            return Ok(regionsDto);
        }


        [HttpPost]
        public IActionResult CreateRegions([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Map or convert RegionDTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageURL = addRegionRequestDTO.RegionImageURL
            };

            //USe Domain model to Create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain Model back to RegionDTO
            var regionsDto = new RegionDTO
            {
                id = regionDomainModel.id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL,
            };

            return CreatedAtAction(nameof(GetRegionsById), new {id = regionsDto.id}, regionsDto);
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateRegions(Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //Check if region Exists
            var regionsDomainModel = dbContext.Regions.FirstOrDefault(x => x.id == id);
            if(regionsDomainModel == null)
            {
                return NotFound();
            }


            //Map or convert RegionDTO to Domain Model
            regionsDomainModel.Code = updateRegionRequestDTO.Code;
            regionsDomainModel.Name = updateRegionRequestDTO.Name;
            regionsDomainModel.RegionImageURL = updateRegionRequestDTO.RegionImageURL;


            //Update using Domain Model
            dbContext.SaveChanges();

            //Map Domain Model back to RegionDTO
            var regionsDto = new RegionDTO
            {
                id = regionsDomainModel.id,
                Code = regionsDomainModel.Code,
                Name = regionsDomainModel.Name,
                RegionImageURL = regionsDomainModel.RegionImageURL,
            };

            return Ok(regionsDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRegions(Guid id)
        {
            //Check if region Exists
            var regionsDomainModel = dbContext.Regions.FirstOrDefault(x => x.id == id);
            if (regionsDomainModel == null)
            {
                return NotFound();
            }


            ////Map or convert RegionDTO to Domain Model
            //regionsDomainModel.Code = updateRegionRequestDTO.Code;
            //regionsDomainModel.Name = updateRegionRequestDTO.Name;
            //regionsDomainModel.RegionImageURL = updateRegionRequestDTO.RegionImageURL;


            //Update using Domain Model
            dbContext.Regions.Remove(regionsDomainModel);
            dbContext.SaveChanges();

            //Map Domain Model back to RegionDTO
            var regionsDto = new RegionDTO
            {
                id = regionsDomainModel.id,
                Code = regionsDomainModel.Code,
                Name = regionsDomainModel.Name,
                RegionImageURL = regionsDomainModel.RegionImageURL,
            };

            return Ok(regionsDto);
        }
    }
}
