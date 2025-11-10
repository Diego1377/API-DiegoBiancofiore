using AutoMapper;
using Flixdi.Application.Dtos.Identity.Roles;
using Flixdi.Entities.MicrosoftIdentity;

namespace Flixdi.WebApi.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }
    }
}
