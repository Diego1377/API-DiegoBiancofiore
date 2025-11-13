using AutoMapper;
using Flixdi.Application;
using Flixdi.Application.Dtos.Actor;
using Flixdi.Application.Dtos.Director;
using Flixdi.Application.Dtos.Pelicula;
using Flixdi.Entities;
using Flixdi.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Flixdi.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PeliculasController> _logger;
        private readonly IApplication<Pelicula> _pelicula;
        private readonly IMapper _mapper;
        public PeliculasController(ILogger<PeliculasController> logger, UserManager<User> userManager, IApplication<Pelicula> pelicula, IMapper mapper)
        {
            _logger = logger;
            _pelicula = pelicula;
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
                return Ok(_mapper.Map<IList<PeliculaResponseDto>>(_pelicula.GetAll()));
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
                var pelicula = _pelicula.GetById(Id.Value);

                if (pelicula is null)
                    return NotFound("Pelicula no encontrado.");

                return Ok(_mapper.Map<PeliculaResponseDto>(pelicula));
            }

            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(PeliculaRequestDto peliculaRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var pelicula = _mapper.Map<Pelicula>(peliculaRequestDto);
            _pelicula.Save(pelicula);
            return Ok(pelicula.Id);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
