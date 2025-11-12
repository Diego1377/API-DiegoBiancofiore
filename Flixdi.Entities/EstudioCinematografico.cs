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
    public class EstudioCinematografico : IEntidad
    {
        public EstudioCinematografico()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [ForeignKey(nameof(Pais))]
        public int IdPais { get; set; }

        public virtual Pais Pais { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }

        #region setters y getters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del Estudio Cinematofrafico no puede estar vacío.");
            Nombre = nombre;
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Nombre);
        }
        #endregion
    }
}
