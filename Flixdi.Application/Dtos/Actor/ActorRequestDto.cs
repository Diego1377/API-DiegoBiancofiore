using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flixdi.Application.Dtos.Actor
{
    public class ActorRequestDto
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }

        [StringLength(30)]
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [ForeignKey(nameof(Pais))]
        public int IdPais { get; set; }
    }
}
