using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Pelicula;
using Flixdi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Flixdi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly ILogger<PeliculasController> _logger;
        private readonly IApplication<Pelicula> _pelicula;
        private readonly IMapper _mapper;
        public PeliculasController(ILogger<PeliculasController> logger, IApplication<Pelicula> pelicula, IMapper mapper)
        {
            _logger = logger;
            _pelicula = pelicula;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<PeliculaResponseDto>>(_pelicula.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Pelicula pelicula = _pelicula.GetById(Id.Value);
            if (pelicula is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PeliculaResponseDto>(pelicula));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PeliculaRequestDto peliculaRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var pelicula = _mapper.Map<Pelicula>(peliculaRequestDto);
            _pelicula.Save(pelicula);
            return Ok(pelicula.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, PeliculaRequestDto peliculaRequestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Pelicula peliculaBack = _pelicula.GetById(Id.Value);
            if (peliculaBack is null) return NotFound();

            peliculaBack = _mapper.Map<Pelicula>(peliculaRequestDto);
            _pelicula.Save(peliculaBack);
            return Ok(peliculaBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Pelicula peliculaBack = _pelicula.GetById(Id.Value);
            if (peliculaBack is null) return NotFound();
            _pelicula.Delete(peliculaBack.Id);
            return Ok();
        }
    }

}
