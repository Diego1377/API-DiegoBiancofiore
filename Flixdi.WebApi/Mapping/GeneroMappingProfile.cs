using AutoMapper;
using Flixdi.Application.Dtos.Genero;
using Flixdi.Entities;

namespace Flixdi.WebApi.Mapping
{
    namespace Peliculas.WebApi.Mapping
    {
        public class GeneroMappingProfile : Profile
        {
            public GeneroMappingProfile()
            {
                CreateMap<Genero, GeneroResponseDto>();
                CreateMap<GeneroRequestDto, Genero>();
            }
        }
    }
}
