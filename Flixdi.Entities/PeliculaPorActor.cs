using Flixdi.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Entities
{
    public class PeliculaPorActor : IEntidad
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Pelicula))]
        public int IdPelicula { get; set; }

        [ForeignKey(nameof(Actor))]
        public int IdActor { get; set; }

        public virtual Pelicula Pelicula { get; set; }
        public virtual Actor Actor { get; set; }
    }
}
