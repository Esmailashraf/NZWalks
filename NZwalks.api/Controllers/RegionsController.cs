using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.api.Data;
using NZwalks.api.Models.Domain;
using NZwalks.api.Models.DTO;

namespace NZwalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbContext _DbContext;
        public RegionsController(NZWalksDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data From Database -DomainMOdels
            var regionsDomain = _DbContext.regions.ToList();
            //Map/Convert From To Dto
            var RegionDto = new List<RegionDto>();

            foreach(var regionDomain in regionsDomain)
            {
                RegionDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            // return DTO to Client
            return Ok(RegionDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Get Data From Database -DomainMOdels
            var regionDomain = _DbContext.regions.Find(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/Convert From To Dto

            var RegionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            // return DTO to Client

            return Ok(RegionDto);

        }
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map/Convert from DTO to Domail model
            var regionDomainModel = new Region()
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            _DbContext.regions.Add(regionDomainModel);
            _DbContext.SaveChanges();
            //convert from Domain Model TO Dto
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateById([FromRoute]Guid id,UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = _DbContext.regions.FirstOrDefault(u => u.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // convert Dto to DomainModel
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            _DbContext.SaveChanges();

            // convert from DomainModel to Dto
            var RegionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(RegionDto);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = _DbContext.regions.FirstOrDefault(u => u.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            _DbContext.regions.Remove(regionDomainModel);
            return Ok();
        }
    }
}