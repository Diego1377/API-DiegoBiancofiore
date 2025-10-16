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
    public class Actor : IEntidad
    {
        public Actor()
        {
            PeliculasPorActores = new HashSet<PeliculaPorActor>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }

        [StringLength(30)]
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [ForeignKey(nameof(Pais))]
        public int IdPais { get; set; }

        public virtual Pais Pais { get; set; }

        public virtual ICollection<PeliculaPorActor> PeliculasPorActores { get; set; }
    }
}
