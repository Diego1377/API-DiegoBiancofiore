using AutoMapper;
using Flixdi.Application.Dtos.Director;
using Flixdi.Entities;

namespace Flixdi.WebApi.Mapping
{
    public class DirectorMappingProfile : Profile
    {
        public DirectorMappingProfile()
        {
            CreateMap<Director, DirectorResponseDto>()
                .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento.ToShortDateString()));

            CreateMap<DirectorRequestDto, Director>();
        }
    }
}
