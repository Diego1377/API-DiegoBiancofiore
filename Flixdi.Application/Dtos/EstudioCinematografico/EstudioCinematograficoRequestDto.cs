using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Application.Dtos.EstudioCinematografico
{
    public class EstudioCinematograficoRequestDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [ForeignKey(nameof(Pais))]
        public int IdPais { get; set; }
    }
}
