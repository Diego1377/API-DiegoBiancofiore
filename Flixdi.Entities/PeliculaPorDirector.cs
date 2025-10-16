using Flixdi.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Entities
{
    public class PeliculaPorDirector : IEntidad
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Pelicula))]
        public int IdPelicula { get; set; }

        [ForeignKey(nameof(Director))]
        public int IdDirector { get; set; }

        public virtual Pelicula Pelicula { get; set; }
        public virtual Director Director { get; set; }
    }
}
