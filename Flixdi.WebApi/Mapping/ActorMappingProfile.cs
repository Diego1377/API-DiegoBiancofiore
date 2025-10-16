using AutoMapper;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Entities;

namespace Flixdi.WebApi.Mapping
{
    public class ActorMappingProfile : Profile
    {
        public ActorMappingProfile()
        {
            CreateMap<Actor, ActorResponseDto>()
                .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento.ToShortDateString()));

            CreateMap<ActorRequestDto, Actor>();
        }
    }
}
