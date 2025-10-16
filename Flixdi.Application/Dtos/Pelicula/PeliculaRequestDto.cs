using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Application.Dtos.Pelicula
{
    public class PeliculaRequestDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public int Duracion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaEstreno { get; set; }
        public bool Activo { get; set; }

        [ForeignKey(nameof(EstudioCinematografico))]
        public int IdEstudioCinematografico { get; set; }
    }
}
