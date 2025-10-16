using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Application.Dtos.Genero
{
    public class GeneroRequestDto
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }
    }
}
