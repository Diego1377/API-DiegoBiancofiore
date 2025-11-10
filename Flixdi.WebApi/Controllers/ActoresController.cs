using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flixdi.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly ILogger<ActoresController> _logger;
        private readonly IApplication<Actor> _actor;
        private readonly IMapper _mapper;

        public ActoresController(ILogger<ActoresController> logger, IApplication<Actor> actor, IMapper mapper)
        {
            _logger = logger;
            _actor = actor;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<ActorResponseDto>>(_actor.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Actor actor = _actor.GetById(Id.Value);
            if (actor is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ActorResponseDto>(actor));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ActorRequestDto actorRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var actor = _mapper.Map<Actor>(actorRequestDto);
            _actor.Save(actor);
            return Ok(actor.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, ActorRequestDto actorRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Actor actorBack = _actor.GetById(Id.Value);
            if (actorBack is null)
            {
                return NotFound();
            }
            actorBack = _mapper.Map<Actor>(actorRequestDto);
            _actor.Save(actorBack);
            return Ok(actorBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) return BadRequest();
            Actor actorBack = _actor.GetById(Id.Value);
            if (actorBack is null)
            {
                return NotFound();
            }
            _actor.Delete(actorBack.Id);
            return Ok();
        }
    }

}
