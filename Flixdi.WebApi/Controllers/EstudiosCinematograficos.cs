using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Application.Dtos.EstudioCinematografico;
using Flixdi.Entities;
using Flixdi.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Flixdi.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosCinematograficosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EstudiosCinematograficosController> _logger;
        private readonly IApplication<EstudioCinematografico> _estudio;
        private readonly IMapper _mapper;

        public EstudiosCinematograficosController(ILogger<EstudiosCinematograficosController> logger, UserManager<User> userManager, IApplication<EstudioCinematografico> estudio, IMapper mapper)
        {
            _logger = logger;
            _estudio = estudio;
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
                    return Ok(_mapper.Map<IList<EstudioCinematograficoResponseDto>>(_estudio.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los estudios cinematofraficos.");
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
                    var estudio = _estudio.GetById(Id.Value);

                    if (estudio is null)
                        return NotFound("Estudio no encontrado.");

                    return Ok(_mapper.Map<EstudioCinematograficoResponseDto>(estudio));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el estudio por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(EstudioCinematograficoRequestDto estudioCinematograficoRequestDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var estudio = _mapper.Map<EstudioCinematografico>(estudioCinematograficoRequestDto);
                _estudio.Save(estudio);
                return Ok(estudio.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el estudio.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, EstudioCinematograficoRequestDto estudioCinematograficoRequestDto)
        {
            try
            {
                if (!Id.HasValue) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();
                EstudioCinematografico estudioBack = _estudio.GetById(Id.Value);
                if (estudioBack is null) return NotFound();
                estudioBack = _mapper.Map<EstudioCinematografico>(estudioCinematograficoRequestDto);
                _estudio.Save(estudioBack);
                return Ok(estudioBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el estudio.");
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
                EstudioCinematografico estudioBack = _estudio.GetById(Id.Value);
                if (estudioBack is null) return NotFound();
                _estudio.Delete(estudioBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el estudio.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }

}
