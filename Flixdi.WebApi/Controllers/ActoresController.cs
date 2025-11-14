using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Application.Dtos.Pelicula;
using Flixdi.Entities;
using Flixdi.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Flixdi.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ActoresController> _logger;
        private readonly IApplication<Actor> _actor;
        private readonly IMapper _mapper;

        public ActoresController(ILogger<ActoresController> logger, UserManager<User> userManager, IApplication<Actor> actor, IMapper mapper)
        {
            _logger = logger;
            _actor = actor;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> All()
        {
            try
            {
                var id = User.FindFirst("Id").Value.ToString();
                var user = _userManager.FindByIdAsync(id).Result;
                if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                    await _userManager.IsInRoleAsync(user, "Cliente"))
                {
                    var name = User.FindFirst("name");
                    var a = User.Claims;
                    return Ok(_mapper.Map<IList<ActorResponseDto>>(_actor.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los actores.");
                return StatusCode(500, "Ocurrió un error al proceprocesar la solicitud.");
            }
        }


        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ById(int? Id)
        {
            try
            {
                if (!Id.HasValue)
                    return BadRequest("Debe especificar un Id.");
                var idUser = User.FindFirst("Id")?.Value;
                var user = await _userManager.FindByIdAsync(idUser);

                if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                    await _userManager.IsInRoleAsync(user, "Cliente"))
                {
                    var actor = _actor.GetById(Id.Value);

                    if (actor is null)
                        return NotFound("Actor no encontrado.");

                    return Ok(_mapper.Map<ActorResponseDto>(actor));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el actor por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(ActorRequestDto actorRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var actor = _mapper.Map<Actor>(actorRequestDto);
                _actor.Save(actor);
                return Ok(actor.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el actor.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, ActorRequestDto actorRequestDto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el actor.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el actor.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
