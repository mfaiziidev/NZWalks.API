using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomValidationAttribute;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        [Route("/api/AddWalk")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            //Map DTO to DomainModel using autoMapper
            var WalkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);

            //Call WalkRepository to Create Walks
            await walkRepository.CreateAsync(WalkDomainModel);

            //Map DomainModel back to DTO using AutoMapper
            var WalkDTO = mapper.Map<WalkDTO>(WalkDomainModel);

            return Ok(WalkDTO);
        }

        [HttpGet]
        [Route("/api/GetAllWalk")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? SortBy, [FromQuery] bool isAscending)
        {
            //Get Data from Repository First
            var WalkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, SortBy, isAscending);

            //Map WalkDomainModel to DTO using AutoMapper
            var WalkDTO = mapper.Map<List<WalkDTO>>(WalkDomainModel);
            
            return Ok(WalkDTO);
        }

        [HttpGet]
        [Route("/api/GetWalkById/{id}")]
        public async Task<IActionResult> GetWalksbyId(Guid id)
        {
            //Get Data from Repository First
            var WalkDomainModel = await walkRepository.GetByIdAsync(id);

            if(WalkDomainModel == null)
            {
                return NotFound();
            }

            //Map WalkDomainModel to DTO using AutoMapper
            var WalkDTO = mapper.Map<WalkDTO>(WalkDomainModel);

            return Ok(WalkDTO);
        }

        [HttpPut]
        [Route("/api/UpdateWalk/{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            //Map DTOs to WalkDomainModel using AutoMapper
            var WalkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);

            //Check if Walk Exists using regionRepository
            var ExistingWalkFromDomainModel = await walkRepository.UpdateAsync(WalkDomainModel, id);

            if (ExistingWalkFromDomainModel == null)
            { return NotFound(); }

            //Map WalkDomainModel to DTO using AutoMapper
            var WalkDTO = mapper.Map<WalkDTO>(ExistingWalkFromDomainModel);
            return Ok(WalkDTO);
        }

        [HttpDelete]
        [Route("/api/DeleteWalk/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //Check if Walk Exists using regionRepository and delete directly
            var walksDomainModel = await walkRepository.DeleteByIdAsync(id);

            //Map back RegionDomainModel to DTO

            var WalkDTO = mapper.Map<WalkDTO>(walksDomainModel);
            return Ok(WalkDTO);
        }
    }
}
