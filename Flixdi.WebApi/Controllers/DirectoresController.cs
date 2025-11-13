using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Application.Dtos.Director;
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
    public class DirectoresController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DirectoresController> _logger;
        private readonly IApplication<Director> _director;
        private readonly IMapper _mapper;
        public DirectoresController(ILogger<DirectoresController> logger, UserManager<User> userManager, IApplication<Director> director, IMapper mapper)
        {
            _logger = logger;
            _director = director;
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
                return Ok(_mapper.Map<IList<DirectorResponseDto>>(_director.GetAll()));
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
                var diretor = _director.GetById(Id.Value);

                if (diretor is null)
                    return NotFound("Director no encontrado.");

                return Ok(_mapper.Map<DirectorResponseDto>(diretor));
            }

            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(DirectorRequestDto directorRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var director = _mapper.Map<Director>(directorRequestDto);
            _director.Save(director);
            return Ok(director.Id);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
