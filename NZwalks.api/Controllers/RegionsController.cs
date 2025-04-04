using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.api.Data;
using NZwalks.api.Models.Domain;
using NZwalks.api.Models.DTO;
using NZwalks.api.Repositories;

namespace NZwalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbContext _DbContext;
        private readonly IRegionRepository iRegionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext DbContext, IRegionRepository iRegionRepository, IMapper mapper)
        {
            _DbContext = DbContext;
            this.iRegionRepository = iRegionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database -DomainMOdels
            var regionsDomain = await iRegionRepository.GetAllAsync();
            //Map/Convert From To Dto

            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Data From Database -DomainMOdels
            var regionDomain = await iRegionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //Map/Convert from DTO to Domail model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            regionDomainModel = await iRegionRepository.CreateAsync(regionDomainModel);

            //convert from Domain Model TO Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map/Convert from DTO to Domail model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await iRegionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }



            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await iRegionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}