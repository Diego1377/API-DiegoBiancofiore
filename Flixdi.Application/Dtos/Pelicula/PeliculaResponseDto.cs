using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Application.Dtos.Pelicula
{
    public class PeliculaResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public DateTime FechaEstreno { get; set; }
        public bool Activo { get; set; }
        public int IdEstudioCinematografico { get; set; }
    }
}
