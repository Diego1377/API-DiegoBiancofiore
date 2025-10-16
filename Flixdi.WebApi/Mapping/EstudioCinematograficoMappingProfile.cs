using AutoMapper;
using Flixdi.Application.Dtos.EstudioCinematografico;
using Flixdi.Entities;

namespace Flixdi.WebApi.Mapping
{
    public class EstudioCinematograficoMappingProfile : Profile
    {
        public EstudioCinematograficoMappingProfile()
        {
            CreateMap<EstudioCinematografico, EstudioCinematograficoResponseDto>();
            CreateMap<EstudioCinematograficoRequestDto, EstudioCinematografico>();
        }
    }
}
