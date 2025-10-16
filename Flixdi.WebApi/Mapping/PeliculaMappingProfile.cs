using AutoMapper;
using Flixdi.Application.Dtos.Pelicula;
using Flixdi.Entities;

namespace Flixdi.WebApi.Mapping
{
    public class PeliculaMappingProfile : Profile
    {
        public PeliculaMappingProfile()
        {
            CreateMap<Pelicula, PeliculaResponseDto>();

            CreateMap<PeliculaRequestDto, Pelicula>();
        }
    }
}
