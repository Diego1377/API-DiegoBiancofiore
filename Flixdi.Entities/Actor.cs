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
       
        
        #region setters y getters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del Actor no puede estar vacío.");
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido del Actor no puede estar vacío.");
            Apellido = apellido;
        }

        public string GetCompleteName()
        {
            return string.Join(", ", Apellido, Nombre);
        }
        #endregion
    }
}
