using AutoMapper;
using Flixdi.Application.Dtos.Pais;
using Flixdi.Entities;

namespace Flixdi.WebApi.Mapping
{
    public class PaisMappingProfile : Profile
    {
        public PaisMappingProfile()
        {
            CreateMap<Pais, PaisResponseDto>();
            CreateMap<PaisRequestDto, Pais>();
        }
    }
}
