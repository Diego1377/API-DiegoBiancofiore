using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Application.Dtos.Director;
using Flixdi.Application.Dtos.Genero;
using Flixdi.Entities;
using Flixdi.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Numerics;

namespace Flixdi.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<GenerosController> _logger;
        private readonly IApplication<Genero> _genero;
        private readonly IMapper _mapper;

        public GenerosController(ILogger<GenerosController> logger, UserManager<User> userManager, IApplication<Genero> genero, IMapper mapper)
        {
            _logger = logger;
            _genero = genero;
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
                    return Ok(_mapper.Map<IList<GeneroResponseDto>>(_genero.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los generos.");
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
                    var genero = _genero.GetById(Id.Value);

                    if (genero is null)
                        return NotFound("Genero no encontrado.");

                    return Ok(_mapper.Map<GeneroResponseDto>(genero));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el genero por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(GeneroRequestDto generoRequestDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var genero = _mapper.Map<Genero>(generoRequestDto);
                _genero.Save(genero);
                return Ok(genero.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el genero.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, GeneroRequestDto generoRequestDto)
        {
            try
            {
                if (!Id.HasValue) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();
                Genero generoBack = _genero.GetById(Id.Value);
                if (generoBack is null) return NotFound();
                generoBack = _mapper.Map<Genero>(generoRequestDto);
                _genero.Save(generoBack);
                return Ok(generoBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el genero.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {

            try
            {
                if (!Id.HasValue) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();
                Genero generoBack = _genero.GetById(Id.Value);
                if (generoBack is null) return NotFound();
                _genero.Delete(generoBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el genero.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }

        }
    }

}
