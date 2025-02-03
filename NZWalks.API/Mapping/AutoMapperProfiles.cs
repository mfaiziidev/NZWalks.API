using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();

            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();            
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();

            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<AddDifficultyRequestDTO, Difficulty>().ReverseMap();
            CreateMap<UpdateDifficultyRequestDTO, Difficulty>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<AddUserRequestDTO, User>().ReverseMap();
            CreateMap<UpdateUserRequestDTO, User>().ReverseMap();
            //CreateMap<List<User>, List<UserDTO>>().ReverseMap();
            CreateMap<User, UserDTO>();
    //.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
            CreateMap<Role, RoleDTO>();


            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<AddRoleRequestDTO, Role>().ReverseMap();
        }
    }
}
