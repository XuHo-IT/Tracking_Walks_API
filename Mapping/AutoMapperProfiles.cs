using AutoMapper;
using NZWalk.API.Model.Domain;
using NZWalk.API.Model.DTO;

namespace NZWalk.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalksRequestDTO, Walk>().ReverseMap(); // Corrected line
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Diffilculty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();
        }
    }
}
