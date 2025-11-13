using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Application.Dtos.Pais;
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
    public class PaisesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PaisesController> _logger;
        private readonly IApplication<Pais> _pais;
        private readonly IMapper _mapper;

        public PaisesController(ILogger<PaisesController> logger, UserManager<User> userManager, IApplication<Pais> pais, IMapper mapper)
        {
            _logger = logger;
            _pais = pais;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> All()
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                await _userManager.IsInRoleAsync(user, "Cliente"))
            {
                var name = User.FindFirst("name");
                var a = User.Claims;
                return Ok(_mapper.Map<IList<PaisResponseDto>>(_pais.GetAll()));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest("Debe especificar un Id.");

            var idUser = User.FindFirst("Id")?.Value;
            var user = await _userManager.FindByIdAsync(idUser);

            if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                await _userManager.IsInRoleAsync(user, "Cliente"))
            {
                var pais = _pais.GetById(Id.Value);
                if (pais is null)
                    return NotFound("País no encontrado.");

                return Ok(_mapper.Map<PaisResponseDto>(pais));
            }

            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(PaisRequestDto paisRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var pais = _mapper.Map<Pais>(paisRequestDto);
            _pais.Save(pais);
            return Ok(pais.Id);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
