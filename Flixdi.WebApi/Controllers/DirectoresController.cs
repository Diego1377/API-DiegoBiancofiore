using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Director;
using Flixdi.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flixdi.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoresController : ControllerBase
    {
        private readonly ILogger<DirectoresController> _logger;
        private readonly IApplication<Director> _director;
        private readonly IMapper _mapper;
        public DirectoresController(ILogger<DirectoresController> logger, IApplication<Director> director, IMapper mapper)
        {
            _logger = logger;
            _director = director;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<DirectorResponseDto>>(_director.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            Director director = _director.GetById(Id.Value);
            if (director is null) return NotFound();
            return Ok(_mapper.Map<DirectorResponseDto>(director));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DirectorRequestDto directorRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var director = _mapper.Map<Director>(directorRequestDto);
            _director.Save(director);
            return Ok(director.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, DirectorRequestDto directorRequestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Director directorBack = _director.GetById(Id.Value);
            if (directorBack is null) return NotFound();

            directorBack = _mapper.Map<Director>(directorRequestDto);
            _director.Save(directorBack);
            return Ok(directorBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Director directorBack = _director.GetById(Id.Value);
            if (directorBack is null) return NotFound();
            _director.Delete(directorBack.Id);
            return Ok();
        }
    }

}
