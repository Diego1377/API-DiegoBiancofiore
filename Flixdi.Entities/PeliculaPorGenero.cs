using Flixdi.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Entities
{
    public class PeliculaPorGenero : IEntidad
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Pelicula))]
        public int IdPelicula { get; set; }

        [ForeignKey(nameof(Genero))]
        public int IdGenero { get; set; }

        public virtual Pelicula Pelicula { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
