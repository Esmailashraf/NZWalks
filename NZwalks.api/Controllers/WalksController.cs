using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.api.Models.Domain;
using NZwalks.api.Models.DTO;
using NZwalks.api.Repositories;

namespace NZwalks.api.Controllers
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
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // convert from Dto to DomainModel
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);

            // map DomainModel to Dto
            return Ok(mapper.Map<WalksDto>(walkDomainModel));

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            //convert from DomainModel To DTO
            return Ok(mapper.Map<List<WalksDto>>(walksDomainModel));
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if(walkDomainModel==null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalksDto>(walkDomainModel));

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var DeletedDomainModel  = await walkRepository.DeleteAsync(id);
            if (DeletedDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalksDto>(DeletedDomainModel));

        }

    }
}
