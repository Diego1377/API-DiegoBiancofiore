using Flixdi.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Flixdi.Entities
{
    public class Pelicula : IEntidad
    {
        public Pelicula()
        {
            PeliculasPorGeneros = new HashSet<PeliculaPorGenero>();
            PeliculasPorActores = new HashSet<PeliculaPorActor>();
            PeliculasPorDirectores = new HashSet<PeliculaPorDirector>();

        }

        public int Id { get ; set; }

        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public int Duracion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaEstreno { get; set; }
        public bool Activo { get; set; }

        [ForeignKey(nameof(EstudioCinematografico))]
        public int IdEstudioCinematografico { get; set; }

        public virtual EstudioCinematografico EstudioCinematografico { get; set; }
        public virtual ICollection<PeliculaPorGenero> PeliculasPorGeneros { get; set; }
        public virtual ICollection<PeliculaPorActor> PeliculasPorActores { get; set; }
        public virtual ICollection<PeliculaPorDirector> PeliculasPorDirectores { get; set; }

        #region setters y getters
        public void SetTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título de la película no puede estar vacío.");
            Titulo = titulo;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede estar vacía.");
            Descripcion = descripcion;
        }

        public void SetDuracion(int duracion)
        {
            if (duracion <= 0)
                throw new ArgumentException("La duración debe ser un número positivo.");
            Duracion = duracion;
        }

        public string GetClassName()
        {
            return string.Join(": ", this.GetType().Name, Titulo);
        }
        #endregion

    }
}
