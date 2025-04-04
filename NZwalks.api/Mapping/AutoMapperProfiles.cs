using AutoMapper;
using NZwalks.api.Models.Domain;
using NZwalks.api.Models.DTO;

namespace NZwalks.api.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

        }
    }
}
