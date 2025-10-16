using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Pais;
using Flixdi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Flixdi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly ILogger<PaisesController> _logger;
        private readonly IApplication<Pais> _pais;
        private readonly IMapper _mapper;

        public PaisesController(ILogger<PaisesController> logger, IApplication<Pais> pais, IMapper mapper)
        {
            _logger = logger;
            _pais = pais;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<PaisResponseDto>>(_pais.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            Pais pais = _pais.GetById(Id.Value);
            if (pais is null) return NotFound();
            return Ok(_mapper.Map<PaisResponseDto>(pais));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PaisRequestDto paisRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var pais = _mapper.Map<Pais>(paisRequestDto);
            _pais.Save(pais);
            return Ok(pais.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, PaisRequestDto paisRequestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Pais paisBack = _pais.GetById(Id.Value);
            if (paisBack is null) return NotFound();
            paisBack = _mapper.Map<Pais>(paisRequestDto);
            _pais.Save(paisBack);
            return Ok(paisBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Pais paisBack = _pais.GetById(Id.Value);
            if (paisBack is null) return NotFound();
            _pais.Delete(paisBack.Id);
            return Ok();
        }
    }

}
