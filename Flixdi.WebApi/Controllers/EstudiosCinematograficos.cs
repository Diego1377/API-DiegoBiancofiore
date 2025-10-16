using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.EstudioCinematografico;
using Flixdi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Flixdi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosCinematograficosController : ControllerBase
    {
        private readonly ILogger<EstudiosCinematograficosController> _logger;
        private readonly IApplication<EstudioCinematografico> _estudio;
        private readonly IMapper _mapper;

        public EstudiosCinematograficosController(ILogger<EstudiosCinematograficosController> logger, IApplication<EstudioCinematografico> estudio, IMapper mapper)
        {
            _logger = logger;
            _estudio = estudio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<EstudioCinematograficoResponseDto>>(_estudio.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            EstudioCinematografico estudio = _estudio.GetById(Id.Value);
            if (estudio is null) return NotFound();
            return Ok(_mapper.Map<EstudioCinematograficoResponseDto>(estudio));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EstudioCinematograficoRequestDto estudioCinematograficoRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var estudio = _mapper.Map<EstudioCinematografico>(estudioCinematograficoRequestDto);
            _estudio.Save(estudio);
            return Ok(estudio.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, EstudioCinematograficoRequestDto estudioCinematograficoRequestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            EstudioCinematografico estudioBack = _estudio.GetById(Id.Value);
            if (estudioBack is null) return NotFound();
            estudioBack = _mapper.Map<EstudioCinematografico>(estudioCinematograficoRequestDto);
            _estudio.Save(estudioBack);
            return Ok(estudioBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            EstudioCinematografico estudioBack = _estudio.GetById(Id.Value);
            if (estudioBack is null) return NotFound();
            _estudio.Delete(estudioBack.Id);
            return Ok();
        }
    }

}
