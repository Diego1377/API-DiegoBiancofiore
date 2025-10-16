using Flixdi.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Entities
{
    public class Pais : IEntidad
    {
        public Pais()
        {
            Actores = new HashSet<Actor>();
            Directores = new HashSet<Director>();
            Estudios = new HashSet<EstudioCinematografico>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<Actor> Actores { get; set; }
        public virtual ICollection<Director> Directores { get; set; }
        public virtual ICollection<EstudioCinematografico> Estudios { get; set; }
    }
}
