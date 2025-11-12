using Flixdi.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.Entities
{
    public class Genero : IEntidad
    {
        public Genero()
        {
            PeliculasPorGeneros = new HashSet<PeliculaPorGenero>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<PeliculaPorGenero> PeliculasPorGeneros { get; set; }

        #region setters y getters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del Genero no puede estar vacío.");
            Nombre = nombre;
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Nombre);
        }
        #endregion
    }
}
